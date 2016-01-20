using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGloboXamarin.Models
{
    public class CustomMenuIcon
    {
        public CustomMenuIcon()
        {
        }

        [Column("Id"), PrimaryKey, NotNull]
        public int Id { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("FatherID")]
        public int FatherId { get; set; }

        [Column("Action")]
        public string Action { get; set; }

        [Column("Icon")]
        public string Icon { get; set; }

        [Column("Ord")]
        public int Ord { get; set; }

        [Column("Level")]
        public int Level { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("FAIcon")]
        public string FAIcon { get { return "menu_" + Icon + ".png"; /*FontAwesome.getFAString (Icon);*/} }

    }
}
