using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nature_net
{
    class database_manager
    {

        // Insert Methods
        public static void InsertFeedback(Feedback f)
        {
            naturenet_dataclassDataContext db = GetTableTopDB();
            db.Feedbacks.InsertOnSubmit(f);
			Action a = new Action();
			a.date = DateTime.Now; a.type_id = 1; a.object_type = f.GetType().ToString();
			a.user_id = f.user_id;
            try { db.SubmitChanges(); a.object_id = f.id; db.Actions.InsertOnSubmit(a); db.SubmitChanges(); }
            catch (Exception e) { log.WriteErrorLog(e); }
        }
        public static void InsertCollection(Collection c)
        {
            naturenet_dataclassDataContext db = GetTableTopDB();
            db.Collections.InsertOnSubmit(c);
			Action a = new Action();
			a.date = DateTime.Now; a.type_id = 1; a.object_type = c.GetType().ToString();
			a.user_id = c.user_id;
            try { db.SubmitChanges(); a.object_id = c.id; db.Actions.InsertOnSubmit(a); db.SubmitChanges(); }
            catch (Exception e) { log.WriteErrorLog(e); }
        }
        public static void InsertCollectionContributionMapping(Collection_Contribution_Mapping c)
        {
            naturenet_dataclassDataContext db = GetTableTopDB();
            db.Collection_Contribution_Mappings.InsertOnSubmit(c);
			Action a = new Action();
			a.date = DateTime.Now; a.type_id = 1; a.object_type = c.GetType().ToString();
            try { db.SubmitChanges(); a.object_id = c.id; a.user_id = c.Collection.user_id; db.Actions.InsertOnSubmit(a); db.SubmitChanges(); }
            catch (Exception e) { log.WriteErrorLog(e); }
        }
        public static void InsertDesignIdea(Contribution c, int user_id)
        {
            naturenet_dataclassDataContext db = GetTableTopDB();
            db.Contributions.InsertOnSubmit(c);
			Action a = new Action();
			a.date = DateTime.Now; a.type_id = 1; a.object_type = c.GetType().ToString();
			a.user_id = user_id; a.technical_info = "Design Idea";
            try { db.SubmitChanges(); a.object_id = c.id; db.Actions.InsertOnSubmit(a); db.SubmitChanges(); }
            catch (Exception e) { log.WriteErrorLog(e); }
        }
        public static void InsertUser(User u)
        {
            naturenet_dataclassDataContext db = GetTableTopDB();
            db.Users.InsertOnSubmit(u);
			Action a = new Action(); 
			a.date = DateTime.Now; a.type_id = 1; a.object_type = u.GetType().ToString();
            try { db.SubmitChanges(); a.user_id = 0; a.object_id = u.id;
			db.Actions.InsertOnSubmit(a); db.SubmitChanges(); }
            catch (Exception e) { log.WriteErrorLog(e); }
        }

        public static void InsertInteraction(Interaction_Log i)
        {
            naturenet_dataclassDataContext db = GetTableTopDB();
            db.Interaction_Logs.InsertOnSubmit(i);
            try { db.SubmitChanges(); }
            catch (Exception e) { log.WriteErrorLog(e); }
        }

        public static naturenet_dataclassDataContext GetTableTopDB()
        {
            if (configurations.site_name == "aces")
                return new naturenet_dataclassDataContext(nature_net.Properties.Settings.Default.nature_netConnectionString_aces);
            if (configurations.site_name == "umd")
                return new naturenet_dataclassDataContext(nature_net.Properties.Settings.Default.nature_netConnectionString_umd);
            if (configurations.site_name == "uncc")
                return new naturenet_dataclassDataContext(nature_net.Properties.Settings.Default.nature_netConnectionString_uncc);
            return new naturenet_dataclassDataContext();
        }
    }
}
