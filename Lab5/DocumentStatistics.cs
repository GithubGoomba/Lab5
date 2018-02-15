using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Lab5
{
    [DataContract]
    class DocumentStatistics 
    {
        #region properties
        [DataMember]
        public int DocumentCount { get; set; }
        [DataMember]
        public List<string> Documents { get; set; }
        [DataMember]
        public Dictionary<string, int> WordCounts { get; set; }
        #endregion

        #region constructor
        public DocumentStatistics()
        {
            DocumentCount = 0;
            Documents = new List<string>();
            WordCounts = new Dictionary<string, int>();
        }
        #endregion
    }
}
