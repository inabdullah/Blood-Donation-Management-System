using System;
using System.Collections.Generic;

namespace Blood_Donation_Management_System.EF.Tables;

public partial class Donation
{
    public int DonationId { get; set; }

    public int DonorId { get; set; }

    public DateOnly DonationDate { get; set; }

    public int VolumeMl { get; set; }

    public string CampName { get; set; } = null!;

    public virtual Donor Donor { get; set; } = null!;
}
