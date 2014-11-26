using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Authentication;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Util;
using DotNetOpenAuth;
using DotNetOpenAuth.OAuth2;
using System.IO;

namespace nature_net
{
    public class file_manager
    {
        public static bool download_file(string url, int contribution_id)
        {
            try
            {
                //System.IO.FileStream file_stream = new System.IO.FileStream(
                //            configurations.GetAbsoluteContributionPath() + contribution_id.ToString(), System.IO.FileMode.OpenOrCreate);
                //file_stream.Close();
                string extension = get_extension(url);
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.DownloadFile(url, configurations.GetAbsoluteContributionPath() + contribution_id.ToString() + "." + extension);
                return true;
            }
            catch (Exception e)
            {
                log.WriteErrorLog(e);
                return false;
            }
        }

        public static string get_extension(string url)
        {
            string extension = "";
            string[] parts = url.Split(new char[] { '.' });
            if (parts.Count() > 1)
                if (parts[parts.Count() - 1].Length < 5)
                    extension = parts[parts.Count() - 1];
            return extension;
        }

        public static bool download_file_from_googledrive(string file_name, int contribution_id)
        {
            try
            {
                var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
                provider.ClientIdentifier = configurations.googledrive_client_id;
                provider.ClientSecret = configurations.googledrive_client_secret;
                IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
                DriveService gd_service = new DriveService(authenticator);

                FilesResource.ListRequest list_request = gd_service.Files.List();
                list_request.Q = "title = '" + file_name + "'";
                FileList file_list = list_request.Fetch();
                byte[] transfer_buffer = new byte[configurations.download_buffer_size];
                int downloaded_files_count = 0;
                foreach (Google.Apis.Drive.v2.Data.File f in file_list.Items)
                {
                    if (String.IsNullOrEmpty(f.DownloadUrl)) continue;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(f.DownloadUrl));
                    authenticator.ApplyAuthenticationToRequest(request);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode != HttpStatusCode.OK) continue;
                    System.IO.FileStream file_stream = new System.IO.FileStream(
                        configurations.GetAbsoluteContributionPath() + contribution_id.ToString() + "." + f.FileExtension, System.IO.FileMode.CreateNew);
                    System.IO.Stream response_stream = response.GetResponseStream();
                    int bytes_read = 0;
                    while ((bytes_read = response_stream.Read(transfer_buffer, 0, transfer_buffer.Length)) > 0) { file_stream.Write(transfer_buffer, 0, bytes_read); }
                    downloaded_files_count++;
                    file_stream.Close();
                    response_stream.Close();
                }
                if (downloaded_files_count > 0) return true;
                else return false;
            }
            catch (Exception gd_exc)
            {
                // write log of the exception
                log.WriteErrorLog(gd_exc);
                return false;
            }
        }

        public static void retrieve_and_process_media_changes_from_googledrive()
        {
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
            provider.ClientIdentifier = configurations.googledrive_client_id;
            provider.ClientSecret = configurations.googledrive_client_secret;
            IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
            DriveService gd_service = new DriveService(authenticator);

            string startChangeId = configurations.googledrive_lastchange;
            List<Change> result = new List<Change>();
            ChangesResource.ListRequest request = gd_service.Changes.List();

            if (!String.IsNullOrEmpty(startChangeId))
            {
                request.StartChangeId = startChangeId;
            }
            do
            {
                try
                {
                    ChangeList changes = request.Fetch();
                    configurations.googledrive_lastchange = changes.LargestChangeId;
                    configurations.SaveChangeID();
                    result.AddRange(changes.Items);
                    request.PageToken = changes.NextPageToken;
                }
                catch (Exception e)
                {
                    // write log of the exception
                    log.WriteErrorLog(e);
                    request.PageToken = null;
                }
            } while (!String.IsNullOrEmpty(request.PageToken));

            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            foreach (Change c in result)
            {
                try
                {
                    if (c.Deleted.HasValue)
                        if (c.Deleted.Value)
                        {
                            //delete from database if exists
                            //System.Windows.Forms.MessageBox.Show("Delete Requested: " + c.FileId);
                            IQueryable<Collection_Contribution_Mapping> ccmap;
                            if (c.File != null)
                            {
                                ccmap = from cc in db.Collection_Contribution_Mappings
                                        where cc.Contribution.media_url.Contains(c.File.Title)
                                        select cc;
                                //System.Windows.Forms.MessageBox.Show("The file was not null.");
                            }
                            else
                            {
                                ccmap = from cc in db.Collection_Contribution_Mappings
                                        where cc.Contribution.technical_info.Contains(c.FileId)
                                        select cc;
                                //System.Windows.Forms.MessageBox.Show("Delete Requested: " + c.FileId + "with " + ccmap.Count() + " mapping(s).");
                            }

                            if (ccmap.Count() == 0) continue;
                            string mappings = "";
                            foreach (Collection_Contribution_Mapping ccm in ccmap)
                                mappings = mappings + ";" + ccm.collection_id + "," + ccm.contribution_id + "," + ccm.date.ToString() + "," + ccm.technical_info;
                            db.Collection_Contribution_Mappings.DeleteAllOnSubmit(ccmap);
                            Action del = new Action();
                            del.type_id = 3;
                            del.user_id = ccmap.First().Collection.user_id;
                            del.date = DateTime.Now;
                            del.object_type = "nature_net.Collection_Contribution_Mapping";
                            del.technical_info = mappings;
                            db.Actions.InsertOnSubmit(del);
                            db.SubmitChanges();
                            //System.Windows.Forms.MessageBox.Show("Delete Was Successful.");
                            //delete from hard drive if exists
                            continue;
                        }

                    if (c.File == null)
                        continue;

                    if (c.File.Title == configurations.googledrive_userfiletitle)
                    {
                        file_manager.retrieve_and_process_user_changes_from_googledrive();
                        continue;
                    }

                    if (c.File.Title == configurations.googledrive_ideafiletitle)
                    {
                        file_manager.retrieve_and_process_idea_changes_from_googledrive();
                        continue;
                    }

                    if (c.File.Parents[0].IsRoot.HasValue)
                        if (!c.File.Parents[0].IsRoot.Value || c.File.Parents.Count > 1)
                            continue;

                    if (c.File.Description == null) continue;

                    string desc = c.File.Description;
                    string username = configurations.GetItemFromJSON(desc, "username");
                    if (username == "") continue;

                    var contribution_list = from contrib in db.Contributions
                                            where contrib.media_url.Contains(c.File.Title)
                                            select contrib;
                    if (contribution_list.Count() == 0)
                    {
                        Contribution contribute = new Contribution();
                        contribute.media_url = c.File.Title;
                        contribute.technical_info = c.File.Id;
                        DateTime dt = DateTime.Now;
                        bool hasdate = DateTime.TryParse(c.File.CreatedDate, out dt);
                        contribute.date = dt;
                        int location_id = 0;
                        try { location_id = Convert.ToInt32(configurations.GetItemFromJSON(desc, "landmarkId")); }
                        catch (Exception) { location_id = 0; }
                        if (location_id == -1)
                            contribute.location_id = 0;
                        else
                            contribute.location_id = location_id;
                        contribute.note = configurations.GetItemFromJSON(desc, "comment");
                        contribute.tags = configurations.GetItemFromJSON(desc, "categories");
                        db.Contributions.InsertOnSubmit(contribute);
                        db.SubmitChanges();
                        int activity_id = 0;
                        try { activity_id = Convert.ToInt32(configurations.GetItemFromJSON(desc, "activityId")); }
                        catch (Exception) { activity_id = 0; }
                        if (activity_id != 0)
                            activity_id++;
                        string avatar_name = configurations.GetItemFromJSON(desc, "avatarName");
                        if (avatar_name.Substring(avatar_name.Length - 4, 4) != ".png")
                            avatar_name = avatar_name + ".png";
                        int collection_id = configurations.get_or_create_collection(db, username, avatar_name, activity_id, dt);
                        Collection_Contribution_Mapping map = new Collection_Contribution_Mapping();
                        map.collection_id = collection_id;
                        map.contribution_id = contribute.id;
                        map.date = DateTime.Now;
                        db.Collection_Contribution_Mappings.InsertOnSubmit(map);
                        db.SubmitChanges();
                    }
                    else
                    {
                        //sync metadata
                        Contribution c1 = contribution_list.First<Contribution>();
                        int location_id = 0;
                        try { location_id = Convert.ToInt32(configurations.GetItemFromJSON(desc, "landmarkId")); }
                        catch (Exception) { location_id = 0; }
                        if (location_id == -1)
                            c1.location_id = 0;
                        else
                            c1.location_id = location_id;
                        c1.note = configurations.GetItemFromJSON(desc, "comment");
                        c1.tags = configurations.GetItemFromJSON(desc, "categories");
                        db.SubmitChanges();
                        int activity_id = 0;
                        try { activity_id = Convert.ToInt32(configurations.GetItemFromJSON(desc, "activityId")); }
                        catch (Exception) { activity_id = 0; }
                        if (activity_id != 0)
                            activity_id++;
                        var ccm = from cc in db.Collection_Contribution_Mappings
                                  where cc.contribution_id == c1.id
                                  select cc.Collection;
                        Collection c_first = ccm.First<Collection>();
                        if (c_first.activity_id != activity_id)
                        {
                            string avatar_name = configurations.GetItemFromJSON(desc, "avatarName");
                            if (avatar_name.Substring(avatar_name.Length - 4, 4) != ".png")
                                avatar_name = avatar_name + ".png";
                            int collection_id = configurations.get_or_create_collection(db, username, avatar_name, activity_id, c_first.date);
                            Collection_Contribution_Mapping map = new Collection_Contribution_Mapping();
                            map.collection_id = collection_id;
                            map.contribution_id = c1.id;
                            map.date = DateTime.Now;
                            db.Collection_Contribution_Mappings.InsertOnSubmit(map);
                            // delete the old mapping
                            var ccm2 = from cc2 in db.Collection_Contribution_Mappings
                                       where cc2.contribution_id == c1.id && cc2.collection_id == c_first.id
                                       select cc2;
                            db.Collection_Contribution_Mappings.DeleteAllOnSubmit(ccm2);
                            db.SubmitChanges();
                        }
                    }
                }
                catch (Exception ex_)
                {
                    // write log of the exception
                    log.WriteErrorLog(ex_);
                    continue;
                }
            }
        }

        public static void retrieve_and_process_user_changes_from_googledrive()
        {
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
            provider.ClientIdentifier = configurations.googledrive_client_id;
            provider.ClientSecret = configurations.googledrive_client_secret;
            IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
            DriveService gd_service = new DriveService(authenticator);

            FilesResource.ListRequest list_request = gd_service.Files.List();
            list_request.Q = "title = '" + configurations.googledrive_userfiletitle + "'";
            FileList file_list = list_request.Fetch();
            if (file_list.Items.Count == 0) return;
            Google.Apis.Drive.v2.Data.File users_list_file = file_list.Items[0];
            if (String.IsNullOrEmpty(users_list_file.DownloadUrl)) return;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(users_list_file.DownloadUrl));
            authenticator.ApplyAuthenticationToRequest(request);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) return;
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string users_list = reader.ReadToEnd();
            reader.Close();
            List<string> usernames = configurations.GetUserNameList_GDText(users_list);
            List<string> avatars = configurations.GetAvatarList_GDText(users_list);

            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var ru = from u in db.Users
                     where u.id > 0
                     select u.name;
            List<string> users = ru.ToList<string>();
            for (int counter = 0; counter < usernames.Count; counter++)
            {
                if (!users.Contains(usernames[counter]))
                {
                    User u3 = new User();
                    u3.name = usernames[counter]; u3.password = ""; u3.email = "";
                    try { u3.avatar = avatars[counter]; }
                    catch (Exception) { u3.avatar = ""; }
                    db.Users.InsertOnSubmit(u3);
                }
                else
                    users.Remove(usernames[counter]);
            }

            //var du = db.Users.Where(a => users.Contains(a.name));
            //if (du.Count() == 0) return;
            //string udel = "";
            //foreach (User u1 in du)
            //    udel = udel + ";" + u1.id + "," + u1.name + "," + u1.avatar + "," + u1.email + "," + u1.technical_info;
            //Action del = new Action();
            //del.date = DateTime.Now;
            //del.object_type = "nature_net.User";
            //del.user_id = 0;
            //del.type_id = 3;
            //del.technical_info = udel;
            //db.Actions.InsertOnSubmit(del);
            //db.Users.DeleteAllOnSubmit(du);
            db.SubmitChanges();
        }

        public static void retrieve_and_process_idea_changes_from_googledrive()
        {
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
            provider.ClientIdentifier = configurations.googledrive_client_id;
            provider.ClientSecret = configurations.googledrive_client_secret;
            IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
            DriveService gd_service = new DriveService(authenticator);

            FilesResource.ListRequest list_request = gd_service.Files.List();
            list_request.Q = "title = '" + configurations.googledrive_ideafiletitle + "'";
            FileList file_list = list_request.Fetch();
            if (file_list.Items.Count == 0) return;
            Google.Apis.Drive.v2.Data.File ideas_list_file = file_list.Items[0];
            if (String.IsNullOrEmpty(ideas_list_file.DownloadUrl)) return;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(ideas_list_file.DownloadUrl));
            authenticator.ApplyAuthenticationToRequest(request);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) return;
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string ideas_list = reader.ReadToEnd();
            reader.Close();
            List<string> ideas = ideas_list.Split(new Char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            naturenet_dataclassDataContext db = database_manager.GetTableTopDB();
            var r = from c in db.Contributions
                    select c.note;
            List<string> notes = r.ToList<string>();
            foreach (string idea in ideas)
            {
                if (!notes.Contains(configurations.GetItem_GDText(idea, 2)))
                {
                    string username = configurations.GetItem_GDText(idea, 0);
                    string avatarname = configurations.GetItem_GDText(idea, 1);
                    if (avatarname.Substring(avatarname.Length - 4, 4) != ".png")
                        avatarname = avatarname + ".png";
                    int col_id = configurations.get_or_create_collection(db, username, avatarname, 1, DateTime.Now);

                    Contribution design_idea = new Contribution();
                    design_idea.date = DateTime.Now;
                    design_idea.location_id = 0;
                    design_idea.media_url = "";
                    design_idea.note = configurations.GetItem_GDText(idea, 2);
                    design_idea.tags = "design idea";
                    db.Contributions.InsertOnSubmit(design_idea);
                    db.SubmitChanges();

                    Collection_Contribution_Mapping map = new Collection_Contribution_Mapping();
                    map.collection_id = col_id;
                    map.contribution_id = design_idea.id;
                    map.date = DateTime.Now;
                    db.Collection_Contribution_Mappings.InsertOnSubmit(map);
                    db.SubmitChanges();
                }
            }
        }

        public static void add_user_to_googledrive(int user_id, string username, string avatar)
        {
            var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description);
            provider.ClientIdentifier = configurations.googledrive_client_id;
            provider.ClientSecret = configurations.googledrive_client_secret;
            IAuthenticator authenticator = new OAuth2Authenticator<NativeApplicationClient>(provider, googledrive_getauthorization);
            DriveService gd_service = new DriveService(authenticator);

            FilesResource.ListRequest list_request = gd_service.Files.List();
            list_request.Q = "title = '" + configurations.googledrive_userfiletitle + "'";
            FileList file_list = list_request.Fetch();
            if (file_list.Items.Count == 0) return;
            Google.Apis.Drive.v2.Data.File users_list_file = file_list.Items[0];
            string user_file_id = users_list_file.Id;
            if (String.IsNullOrEmpty(users_list_file.DownloadUrl)) return;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(users_list_file.DownloadUrl));
            authenticator.ApplyAuthenticationToRequest(request);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK) return;
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string users_list = reader.ReadToEnd();
            reader.Close();

            string u = "{user: id= " + user_id.ToString() + ", name= " + username + ", avatarName= " + avatar + "}\r\n";
            users_list = users_list + u;
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
            writer.Write(users_list);
            writer.Flush();
            stream.Position = 0;
            
            //users_list_file.Title = newTitle;
            //users_list_file.Description = newDescription;
            string new_mime_type = users_list_file.MimeType;
            FilesResource.UpdateMediaUpload request_update = gd_service.Files.Update(users_list_file, user_file_id, stream, new_mime_type);
            request_update.Upload();
        }

        private static IAuthorizationState googledrive_getauthorization(NativeApplicationClient client)
        {
            string[] scopes = new string[1] { "" };
            IAuthorizationState state = new AuthorizationState(scopes) { RefreshToken = configurations.googledrive_refresh_token };
            if (state != null)
            {
                client.RefreshToken(state);
            }
            return state;
        }
    }
}
