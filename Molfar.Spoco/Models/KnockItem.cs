using Microsoft.WindowsAzure.MobileServices;
using System;

namespace Molfar.Spoco.Models
{
    public class KnockItem
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        [CreatedAt]
        public DateTimeOffset? CreatedAt { get; set; }
        [UpdatedAt]
        public DateTimeOffset? UpdatedAt { get; set; }
    }

}
