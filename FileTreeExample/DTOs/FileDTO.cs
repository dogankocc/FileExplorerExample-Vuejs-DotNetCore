using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTreeExample.DTOs
{
    public class FileDTO
    {
        public Guid ID { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public bool Expand { get; set; }
        public string Extension { get; set; }
        private List<FileDTO> _Directories = new List<FileDTO>();
        public List<FileDTO> Directories { get { return _Directories; } set { _Directories = value; } }
        private List<FileDTO> _Files = new List<FileDTO>();
        public List<FileDTO> Files { get { return _Files; } set { _Files = value; } }
    }
}
