using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using nature_net.user_controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace nature_net
{
    public class list_refresher
    {
        private readonly BackgroundWorker worker_design_ideas = new BackgroundWorker();
        private readonly BackgroundWorker worker_activities = new BackgroundWorker();
        private readonly BackgroundWorker worker_users = new BackgroundWorker();

        List<int> current_design_ideas = new List<int>();
        List<int> current_activities = new List<int>();
        List<int> current_users = new List<int>();
        List<int> current_contributions = new List<int>();

        public list_populator design_ideas_populator;
        public list_populator activities_populator;
        public list_populator users_populator;

        public list_refresher()
        {
            worker_design_ideas.DoWork += new DoWorkEventHandler(get_all_design_ideas);
            worker_design_ideas.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_design_ideas);
            worker_activities.DoWork += new DoWorkEventHandler(get_all_activities);
            worker_activities.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_activities);
            worker_users.DoWork += new DoWorkEventHandler(get_all_users);
            worker_users.RunWorkerCompleted += new RunWorkerCompletedEventHandler(display_all_users);
        }

        public void list_all_design_ideas()
        {
            if (!worker_design_ideas.IsBusy)
                worker_design_ideas.RunWorkerAsync(null);
        }
        void get_all_design_ideas(object arg, DoWorkEventArgs e)
        {
            if (!design_idea_has_something_new())
            {
                e.Result = null;
                return;
            }
            try
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from d in db.Design_Ideas
                        orderby d.date descending
                        select d;
                if (r == null)
                    e.Result = null;
                List<design_idea_item> ideas = new List<design_idea_item>();
                foreach (Design_Idea d in r)
                {
                    DateTime last_time = d.date;
                    var n1 = from f in db.Feedbacks
                             where (f.object_type == "nature_net.Contribution") && (f.object_id == d.id)
                             orderby f.date descending
                             select f;
                    int cnt = 0; int num_like = 0; int num_dislike = 0; int num_comments = 0;
                    if (n1 != null)
                        cnt = n1.Count();
                    if (cnt != 0)
                    {
                        last_time = n1.First().date;
                        foreach (Feedback f2 in n1)
                        {
                            if (f2.Feedback_Type.name == "Like")
                                if (Convert.ToBoolean(f2.note))
                                    num_like++;
                                else
                                    num_dislike++;
                            if (f2.Feedback_Type.name == "Comment")
                                num_comments++;
                        }
                    }
                    design_idea_item i = new design_idea_item();
                    ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + d.avatar));
                    src.Freeze();
                    i.img = src;
                    i.design_idea = d;
                    i.count = num_comments;
                    i.last_date = last_time;
                    i.num_dislike = num_dislike;
                    i.num_like = num_like;
                    ideas.Add(i);
                }
                e.Result = ideas;
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
                e.Result = null;
            }
        }
        void display_all_design_ideas(object di, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null) return;
            design_ideas_populator.display_all_design_ideas(di, e);
        }
        bool design_idea_has_something_new()
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r = from d in db.Design_Ideas
                    select d.id;
            if (r == null)
                return false;
            List<int> new_list = r.ToList<int>();
            // compare the list with current list
            new_list.Sort();
            if (new_list.Count != current_design_ideas.Count)
            {
                current_design_ideas = new_list;
                return true;
            }
            for (int counter=0;counter<current_design_ideas.Count;counter++)
                if (current_design_ideas[counter] != new_list[counter])
                {
                    current_design_ideas = new_list;
                    return true;
                }
            return false;
        }

        public void list_all_activities()
        {
            if (!worker_activities.IsBusy)
                worker_activities.RunWorkerAsync();
        }
        void get_all_activities(object arg, DoWorkEventArgs e)
        {
            if (!activities_has_something_new())
            {
                e.Result = null;
                return;
            }
            try
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from a in db.Activities
                        where (a.name != "Free Observation") && (a.name != "Design Idea")
                        select a;
                if (r != null)
                {
                    //List<Activity> activities = r.ToList<Activity>();
                    List<activity_item> activity_items = new List<activity_item>();
                    foreach (Activity a in r)
                    {
                        DateTime last_time = a.creation_date;
                        var n1 = from m in db.Collection_Contribution_Mappings
                                 where m.Collection.activity_id == a.id
                                 orderby m.Contribution.date descending
                                 select new { m.Contribution.date, m.Collection.User.name };
                        int cnt = 0;
                        if (n1 != null)
                            cnt = n1.Count();

                        activity_item ai = new activity_item();
                        ai.activity = a;
                        ai.count = cnt;
                        if (cnt != 0)
                            ai.username = n1.First().name;
                        else
                            ai.username = "";
                        if (cnt != 0)
                            last_time = n1.First().date;

                        ai.last_date = last_time;
                        activity_items.Add(ai);
                    }
                    e.Result = (object)activity_items;
                }
                else
                {
                    e.Result = null;
                }
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
                e.Result = null;
            }
        }
        void display_all_activities(object arg, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null) return;
            activities_populator.display_all_activities(arg, e);
        }
        bool activities_has_something_new()
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r1 = from a in db.Activities
                    where (a.name != "Free Observation") && (a.name != "Design Idea")
                    select a.id;
            var r2 = from c in db.Contributions
                     select c.id;
            if (r1 == null || r2 == null)
                return false;
            List<int> new_list1 = r1.ToList<int>();
            List<int> new_list2 = r2.ToList<int>();
            // compare the list with current list
            new_list1.Sort(); new_list2.Sort();
            if (new_list1.Count != current_activities.Count || new_list2.Count != current_contributions.Count)
            {
                current_activities = new_list1; current_contributions = new_list2;
                return true;
            }
            bool ret_val = false;
            if (has_something_new(current_activities, new_list1))
            {
                current_activities = new_list1;
                ret_val = true;
            }
            if (has_something_new(current_contributions, new_list2))
            {
                current_contributions = new_list2;
                ret_val = true;
            }
            return ret_val;
        }

        public void list_all_users()
        {
            if (!worker_users.IsBusy)
                worker_users.RunWorkerAsync();
        }
        void get_all_users(object arg, DoWorkEventArgs e)
        {
            if (!users_has_something_new())
            {
                e.Result = null;
                return;
            }
            try
            {
                naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
                var r = from u in db.Users
                        where u.id != 0
                        orderby u.name
                        select u;
                if (r == null)
                    e.Cancel = true;
                List<user_item> users = new List<user_item>();
                foreach (User u in r)
                {
                    var n1 = from m in db.Collection_Contribution_Mappings
                             where m.Collection.user_id == u.id
                             orderby m.Contribution.date descending
                             select m.Contribution.date;
                    //var n2 = from f in db.Feedbacks
                    //         where f.user_id == u.id
                    //         orderby f.date descending
                    //         select f.date;
                    List<DateTime> n2 = null;
                    int cnt = 0;
                    if (n1 != null)
                        cnt = n1.Count();
                    if (n2 != null)
                        cnt = cnt + n2.Count();
                    user_item i = new user_item();
                    ImageSource src = new BitmapImage(new Uri(configurations.GetAbsoluteAvatarPath() + u.avatar));
                    src.Freeze();
                    i.img = src;
                    i.user = u;
                    i.count = cnt;
                    i.has_date = false;
                    if (n1 != null)
                    {
                        if (n1.Count() > 0)
                        {
                            i.last_date = n1.First();
                            i.has_date = true;
                        }
                    }
                    if (n2 != null)
                    {
                        if (n2.Count() > 0)
                        {
                            if (i.has_date)
                            {
                                if (i.last_date.CompareTo(n2.First()) < 0)
                                    i.last_date = n2.First();
                            }
                            else
                            {
                                i.last_date = n2.First();
                                i.has_date = true;
                            }
                        }
                    }
                    users.Add(i);
                }
                e.Result = users;
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
                e.Result = null;
            }
        }
        void display_all_users(object us, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null) return;
            users_populator.display_all_users(us, e);
        }
        bool users_has_something_new()
        {
            naturenet_dataclassDataContext db = new naturenet_dataclassDataContext();
            var r1 = from u in db.Users
                    where u.id != 0
                    select u.id;
            var r2 = from c in db.Contributions
                     select c.id;
            if (r1 == null || r2 == null)
                return false;
            List<int> new_list1 = r1.ToList<int>();
            List<int> new_list2 = r2.ToList<int>();
            // compare the list with current list
            new_list1.Sort(); new_list2.Sort();
            if (new_list1.Count != current_users.Count || new_list2.Count != current_contributions.Count)
            {
                current_users = new_list1; current_contributions = new_list2;
                return true;
            }
            bool ret_val = false;
            if (has_something_new(current_users, new_list1))
            {
                current_users = new_list1;
                ret_val = true;
            }
            if (has_something_new(current_contributions, new_list2))
            {
                current_contributions = new_list2;
                ret_val = true;
            }
            return ret_val;
        }

        bool has_something_new(List<int> l1, List<int> l2)
        {
            for (int counter = 0; counter < l1.Count; counter++)
                if (l1[counter] != l2[counter])
                    return true;
            return false;
        }
    }
}
