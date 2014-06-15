using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LTDBAccess;


namespace Answers
{
    public class FindAnswers
    {
        DBHelper dh = new DBHelper();

        public bool GetUserIdVerify(string username)
        {
            bool result = false;
            string strSql = string.Format(@"select userid, username from users where username = '{0}'", username);
            try{
                DataTable dt = dh.ExeSQL(strSql);
                var id_nbr = dt.Rows[0][0];
                string user_name = dt.Rows[0][1].ToString();
                int i = Convert.ToInt16(id_nbr);
                if (i > 0)
                {
                    result = true;
                }
                
            }catch{
                result = false;
            }
            
            return result;
        }

        public string GetReply(string[] keyWords, int keyCount)
        {
            string reply = "你在自言自语些什么啊";
            string strSql =null;

            switch (keyCount)
            {
                case 1: strSql = string.Format(@"select id,answers from conversation where (Questions like '%' +'{0}'+'%')", keyWords[0]);
                    break;
                case 2: strSql = string.Format(@"select id,answers from conversation where (Questions like '%' +'{0}'+'%') and (Questions like '%' +'{1}'+'%')", keyWords[0], keyWords[1]);
                    break;
                case 3: strSql = string.Format(@"select id,answers from conversation where (Questions like '%' +'{0}'+'%') and (Questions like '%' +'{1}'+'%') and (Questions like '%' +'{2}'+'%')", keyWords[0], keyWords[1], keyWords[2]);
                    break;
                case 4: strSql = string.Format(@"select id,answers from conversation where (Questions like '%' +'{0}'+'%') and (Questions like '%' +'{1}'+'%') and (Questions like '%' +'{2}'+'%') and (Questions like '%' +'{3}'+'%')", keyWords[0], keyWords[1], keyWords[2], keyWords[3]);
                    break;
                case 5: strSql = string.Format(@"select id,answers from conversation where (Questions like '%' +'{0}'+'%') and (Questions like '%' +'{1}'+'%') and (Questions like '%' +'{2}'+'%') and (Questions like '%' +'{3}'+'%') and (Questions like '%' +'{4}'+'%')", keyWords[0], keyWords[1], keyWords[2], keyWords[3], keyWords[4]);
                    break;
                case 6: strSql = string.Format(@"select id,answers from conversation where (Questions like '%' +'{0}'+'%') and (Questions like '%' +'{1}'+'%') and (Questions like '%' +'{2}'+'%') and (Questions like '%' +'{3}'+'%') and (Questions like '%' +'{4}'+'%') and (Questions like '%' +'{5}'+'%')", keyWords[0], keyWords[1], keyWords[2], keyWords[3], keyWords[4], keyWords[5]);
                    break;
                default: strSql = string.Format(@"select id,answers from conversation where (Questions like '%' +'{0}'+'%')", keyWords[0]);
                    break;
            }
            try
            {
                DataTable dt = dh.ExeSQL(strSql);
                var id_nbr = dt.Rows[0][0];
                reply = dt.Rows[0][1].ToString();
                int i = Convert.ToInt16(id_nbr);
                if (i > 0)
                {
                    reply = dt.Rows[0][1].ToString();
                }

            }
            catch
            {
                reply = "完了，音律失去了和谐。";
            }
            return reply;
        }

        /// <summary>
        /// Record new things into Brain
        /// </summary>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <returns>false if insert failed and true if success</returns>
        public bool learnSomethingNew(string question, string answer)
        {
            bool result = false;

            string strSql = string.Format(@"INSERT INTO conversation (Questions,answers) VALUES ('{0}','{1}')", question, answer);
            try
            {
                result = dh.ExcuteChangeSql(strSql);
            }
            catch
            {
                result = false;
            }
            return result;

        }

        public bool forNewSettler(string username)
        {
            bool result = false;

            string strSql = string.Format(@"INSERT INTO users (username) VALUES ('{0}')", username);
            try
            {
                result = dh.ExcuteChangeSql(strSql);
            }
            catch
            {
                result = false;
            }
            return result;

        }
    }
}
