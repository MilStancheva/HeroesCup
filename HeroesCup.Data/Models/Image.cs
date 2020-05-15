using System;

namespace HeroesCup.Data.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        public byte[] Bytes { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public Guid? ClubId { get; set; }
        public Club Club { get; set; }
    }
}
