using System;
using System.Collections.Generic;

namespace finalProject.Models1;

public partial class Size
{
    public int SizesId { get; set; }

    public int? SizeDescriptionId { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

    public bool? Along { get; set; }

    public virtual SizesDescription? SizeDescription { get; set; }
}
