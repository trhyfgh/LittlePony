using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanGu;
using System.Data;
using System.Text;
using PanGu.Dict;


namespace littleTian
{
    public class WordSegment
    {
        //public string SimpleSegment(string inputWords, PanGuTokenizer ktTokenizer)libs
        public ICollection<WordInfo> SimpleSegment(string inputWords)
        {
            PanGu.Segment.Init("PanGu.xml");
            Segment segment = new Segment();
            StringBuilder result = new StringBuilder(); 

            //ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(inputWords); 
            ICollection<WordInfo> words = segment.DoSegment(inputWords);
             //foreach (WordInfo word in words)
             //{
             //    if (word == null)
             //    {
             //        continue;
             //    }
             //    result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank)); 
             //}
             return words; 
        }
    }
}