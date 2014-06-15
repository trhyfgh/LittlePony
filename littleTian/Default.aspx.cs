using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Answers;
using PanGu;
using System.Text;
using System.Configuration;
namespace littleTian
{
    public partial class _Default : Page
    {
        int wordCount;
        int keyCount;
        string[] keyWord;
        bool inputFlag;
        bool userIdentified;
        bool pageLoaded = false;
        protected bool hasUser;
        public static string enableNewSettler;
        public static bool attand;
                    
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadUser();
                
            }

        }
        private void LoadUser()
        {
            pageLoaded = true;
            hasUser = false;
            DateTime dt = DateTime.Now;
            string time = string.Format("{0:HH:mm:ss}", dt);
            this.ConversationDisplay.Items.Add("暮光闪闪 " + dt);
            string askVerify = "你就是我的Master吗？";
            this.ConversationDisplay.Items.Add(askVerify);
            this.ConversationDisplay.Items.Add(" ");
            enableNewSettler = System.Configuration.ConfigurationManager.AppSettings["enableNewSettler"];


        }

        protected void submitConv_Click(object sender, EventArgs e)
        {
            keyCount = 0;
            if (Session["hasUser"] == "Y")
            {
                ReplyMe();  
            }
            else{
                DateTime dt = DateTime.Now;
                string time = string.Format("{0:HH:mm:ss}", dt);
                wordCount = this.ConversationSubmit.Text.Length;
                string userId = this.ConversationSubmit.Text;
                this.ConversationDisplay.Items.Add("我  " + dt);
                string conversation = this.ConversationSubmit.Text;
                this.ConversationDisplay.Items.Add(conversation);
                this.ConversationDisplay.Items.Add(" ");
                hasUser = getUserVerify(userId);
                if (hasUser)
                {
                    Session["hasUser"] = "Y";
                }
                else
                {
                    Session["hasUser"] = "N";
                }
                   
                if(hasUser){
                    userIdentified = true;
                    dt = DateTime.Now;
                    time = string.Format("{0:HH:mm:ss}", dt);
                    this.ConversationDisplay.Items.Add("暮光闪闪 " + dt);
                    conversation = "Servant暮光闪闪，听从召唤而来";
                    this.ConversationDisplay.Items.Add(conversation);
                    this.ConversationDisplay.Items.Add(" ");
                }else
                {
                    if (enableNewSettler == "on" && !attand)
                    {
                        string userName = conversation;
                        FindAnswers fa = new FindAnswers();
                        bool settle = fa.forNewSettler(userName);
                        hasUser = getUserVerify(userId);
                        dt = DateTime.Now;
                        time = string.Format("{0:HH:mm:ss}", dt);
                        this.ConversationDisplay.Items.Add("暮光闪闪 " + dt);
                        if (settle && hasUser)
                        {
                            conversation = "欢迎来到小马镇";
                            attand = true;
                            Session["hasUser"] = "Y";
                        }
                        else
                        {
                            conversation = "梦魇之月来了，外来人还是赶快跑吧";
                            attand = false;
                        }
                    }
                    else
                    {
                        userIdentified = false;
                        dt = DateTime.Now;
                        time = string.Format("{0:HH:mm:ss}", dt);
                        this.ConversationDisplay.Items.Add("暮光闪闪 " + dt);
                        conversation = "那是什么？可以吃吗？";

                    }
                    this.ConversationDisplay.Items.Add(conversation);
                    this.ConversationDisplay.Items.Add(" ");
                
                }

            }

            this.ConversationSubmit.Text = "";
            this.ConversationDisplay.SelectedIndex = this.ConversationDisplay.Items.Count - 1;

        }
        private bool getUserVerify(string userid)
        {
            bool result = false;
            try
            {
                WordSegment iWordSegment = new WordSegment();
                ICollection<WordInfo> username = iWordSegment.SimpleSegment(userid);
                foreach (WordInfo word in username)
                {
                    if (word == null)
                    {
                        continue;
                    }
                    if ((int)Math.Pow(3, word.Rank)!=243)
                    {
                        continue;
                    }
                    FindAnswers fa = new FindAnswers();
                    result = fa.GetUserIdVerify(word.Word);
                    if (result)
                    {
                        break;
                    }
                }

            }
            catch
            {
                result = false;
            }

            return result;
        }

        private void ReplyMe()
        {
            IAmReplying();

        }
        /// <summary>
        /// Response to the questions.
        /// </summary>
        private void IAmReplying()
        {
            //bool techModOn = false;
            bool askModOn = false;
            bool rlyModOn = false;
            string reply;
            string mode;

            DateTime dt = DateTime.Now;

            string time = string.Format("{0:HH:mm:ss}", dt);

            this.ConversationDisplay.Items.Add("我  " + dt);
            string conversation = this.ConversationSubmit.Text;
            this.ConversationDisplay.Items.Add(conversation);
            this.ConversationDisplay.Items.Add(" ");
            int wordLenght = conversation.Length;
            if (wordLenght >= 3)
            {
                mode = conversation.Substring(0, 3);
            }
            else
            {
                mode = conversation;
            }
            switch(mode)
            {
                case "ask": askModOn = true; rlyModOn = false; break;
                case "ASK": askModOn = true; rlyModOn = false; break;
                case "rly": askModOn = false; rlyModOn = true; break;
                case "RLY": askModOn = false; rlyModOn = true; break;

                default: askModOn = false; rlyModOn = false; break;
            }

            if(askModOn)
            {
                dt = DateTime.Now;
                time = string.Format("{0:HH:mm:ss}", dt);
                this.ConversationDisplay.Items.Add("暮光闪闪 " + dt);
                Session["question"] = conversation.Substring(3).Trim();
                reply = "我在认真的听着，请告诉我答案吧"; 
                this.ConversationDisplay.Items.Add(reply);
                this.ConversationDisplay.Items.Add(" ");
            }
            else if (rlyModOn)
            {
                dt = DateTime.Now;
                time = string.Format("{0:HH:mm:ss}", dt);
                this.ConversationDisplay.Items.Add("暮光闪闪 " + dt);
                Session["answer"] = conversation.Substring(3).Trim();
                //Session["switch"] = "answer";
                bool remeber = false;
                FindAnswers fa = new FindAnswers();
                remeber = fa.learnSomethingNew(Session["question"].ToString(), Session["answer"].ToString());
                reply = "我记住了";
                this.ConversationDisplay.Items.Add(reply);
                this.ConversationDisplay.Items.Add(" ");

            }
            else
            {
                WordSegment iWordSegment = new WordSegment();
                ICollection<WordInfo> segWords = iWordSegment.SimpleSegment(conversation);
                int i = 0;
                StringBuilder temString = new StringBuilder();
                foreach (WordInfo word in segWords)
                {
                    if (word == null)
                    {
                        continue;
                    }
                    if ((int)Math.Pow(3, word.Rank) != 243)
                    {
                        continue;
                    }
                    temString.AppendFormat("{0}{1}", word.Word, '/');
                }
                string sb = temString.ToString();
                int start = 0;
                keyCount = 0;
                while (true)
                {
                    int z = sb.IndexOf("/", start);
                    if (z != -1)
                    {
                        keyCount++;
                        start = z + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                if (keyCount == 0)
                {
                    keyCount = 1;
                }
                //string reply;
                if (keyCount > 6)
                {
                    reply = "我在吃东西，你说了什么吗？";
                }
                else
                {
                    keyWord = sb.Split('/');
                    FindAnswers fa = new FindAnswers();
                    //ssb = sb.Substring(0, keyCount + keyCount - 1);
                    reply = fa.GetReply(keyWord, keyCount);
                }
                dt = DateTime.Now;
                time = string.Format("{0:HH:mm:ss}", dt);
                this.ConversationDisplay.Items.Add("暮光闪闪 " + dt);
                this.ConversationDisplay.Items.Add(reply);
                this.ConversationDisplay.Items.Add(" ");

            }
            
        }
     
    }
}