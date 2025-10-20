using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace finalProject.Models1;

public partial class AdCycContext : DbContext
{
    public AdCycContext()
    {
    }

    public AdCycContext(DbContextOptions<AdCycContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ad> Ads { get; set; }

    public virtual DbSet<AdType> AdTypes { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<ChargesPayment> ChargesPayments { get; set; }

    public virtual DbSet<CommunicationCustomer> CommunicationCustomers { get; set; }

    public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }

    public virtual DbSet<CommunicationUser> CommunicationUsers { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerCharge> CustomerCharges { get; set; }

    public virtual DbSet<CustomersType> CustomersTypes { get; set; }

    public virtual DbSet<EngagedDetail> EngagedDetails { get; set; }

    public virtual DbSet<GetUsersDetails1> GetUsersDetails1s { get; set; }

    public virtual DbSet<GetUsersDetails2> GetUsersDetails2s { get; set; }

    public virtual DbSet<GetUsersDetails3> GetUsersDetails3s { get; set; }

    public virtual DbSet<GetUsersDetails4> GetUsersDetails4s { get; set; }

    public virtual DbSet<IshurForAd> IshurForAds { get; set; }

    public virtual DbSet<Ishurim> Ishurims { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<PaymentsType> PaymentsTypes { get; set; }

    public virtual DbSet<PriceList> PriceLists { get; set; }

    public virtual DbSet<PublicationDate> PublicationDates { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<SizesDescription> SizesDescriptions { get; set; }

    public virtual DbSet<StatusAd> StatusAds { get; set; }

    public virtual DbSet<StatusIshur> StatusIshurs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=COMP;Initial Catalog=AD_CYC;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ad>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__Ad__7130D58E529A80B0");

            entity.ToTable("Ad");

            entity.Property(e => e.AdId).HasColumnName("AdID");
            entity.Property(e => e.AdStatusId).HasColumnName("AdStatusID");
            entity.Property(e => e.AdTypeId).HasColumnName("AdTypeID");
            entity.Property(e => e.Content)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.DateOrder).HasColumnType("datetime");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.SectionId).HasColumnName("SectionID");
            entity.Property(e => e.SizeId).HasColumnName("SizeID");

            entity.HasOne(d => d.AdStatus).WithMany(p => p.Ads)
                .HasForeignKey(d => d.AdStatusId)
                .HasConstraintName("FK__Ad__AdStatusID__08B54D69");

            entity.HasOne(d => d.AdType).WithMany(p => p.Ads)
                .HasForeignKey(d => d.AdTypeId)
                .HasConstraintName("FK__Ad__AdTypeID__09A971A2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Ads)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Ad__CustomerId__07C12930");

            entity.HasOne(d => d.Location).WithMany(p => p.Ads)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Ad__LocationID__0B91BA14");

            entity.HasOne(d => d.Section).WithMany(p => p.Ads)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK__Ad__SectionID__0C85DE4D");

            entity.HasOne(d => d.Size).WithMany(p => p.Ads)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__Ad__SizeID__0A9D95DB");
        });

        modelBuilder.Entity<AdType>(entity =>
        {
            entity.HasKey(e => e.AdTypeId).HasName("PK__AdTypes__3B1564B96CB9EA70");

            entity.Property(e => e.AdTypeId).HasColumnName("AdTypeID");
            entity.Property(e => e.DescriptionAdTypes)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PriceAdType).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.CharactersId).HasName("PK__Characte__1FB9CBA8752B911E");

            entity.Property(e => e.CharactersId).HasColumnName("charactersID");
            entity.Property(e => e.CharactersName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("charactersName");
        });

        modelBuilder.Entity<ChargesPayment>(entity =>
        {
            entity.HasKey(e => e.ChargesPaymentsId).HasName("PK__ChargesP__15E7670479861580");

            entity.Property(e => e.ChargesPaymentsId).HasColumnName("ChargesPaymentsID");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

            entity.HasOne(d => d.Charge).WithMany(p => p.ChargesPayments)
                .HasForeignKey(d => d.ChargeId)
                .HasConstraintName("FK__ChargesPa__Charg__3493CFA7");

            entity.HasOne(d => d.Payment).WithMany(p => p.ChargesPayments)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__ChargesPa__Payme__3587F3E0");
        });

        modelBuilder.Entity<CommunicationCustomer>(entity =>
        {
            entity.HasKey(e => e.CommunicationId).HasName("PK__Communic__53E565EF47A20027");

            entity.Property(e => e.Contant)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contant");

            entity.HasOne(d => d.CommunicationTypeNavigation).WithMany(p => p.CommunicationCustomers)
                .HasForeignKey(d => d.CommunicationType)
                .HasConstraintName("FK__Communica__Commu__25518C17");

            entity.HasOne(d => d.Customer).WithMany(p => p.CommunicationCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Communica__Custo__245D67DE");
        });

        modelBuilder.Entity<CommunicationType>(entity =>
        {
            entity.HasKey(e => e.CommunicationType1).HasName("PK__Communic__3CCB282CC9C0978E");

            entity.ToTable("CommunicationType");

            entity.Property(e => e.CommunicationType1)
                .ValueGeneratedNever()
                .HasColumnName("CommunicationType");
            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CommunicationUser>(entity =>
        {
            entity.HasKey(e => e.CommunicationId).HasName("PK__Communic__53E565EF442908F8");

            entity.ToTable("CommunicationUser");

            entity.Property(e => e.Contant)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contant");

            entity.HasOne(d => d.CommunicationTypeNavigation).WithMany(p => p.CommunicationUsers)
                .HasForeignKey(d => d.CommunicationType)
                .HasConstraintName("FK__Communica__Commu__1CBC4616");

            entity.HasOne(d => d.User).WithMany(p => p.CommunicationUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Communica__UserI__1BC821DD");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D811688517");

            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CustomerCreditBalance).HasColumnType("money");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Identification)
                .HasMaxLength(9)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.CustomerTypeNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CustomerType)
                .HasConstraintName("FK__Customers__Custo__6A30C649");
        });

        modelBuilder.Entity<CustomerCharge>(entity =>
        {
            entity.HasKey(e => e.ChargeId).HasName("PK__Customer__17FC361B00A09F56");

            entity.Property(e => e.ChargeAmount).HasColumnType("money");
            entity.Property(e => e.ChargeDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerCharges)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__CustomerC__Custo__2FCF1A8A");

            entity.HasOne(d => d.PaymentStatusNavigation).WithMany(p => p.CustomerCharges)
                .HasForeignKey(d => d.PaymentStatus)
                .HasConstraintName("FK__CustomerC__Payme__31B762FC");

            entity.HasOne(d => d.PublicationDates).WithMany(p => p.CustomerCharges)
                .HasForeignKey(d => d.PublicationDatesId)
                .HasConstraintName("FK__CustomerC__Publi__30C33EC3");
        });

        modelBuilder.Entity<CustomersType>(entity =>
        {
            entity.HasKey(e => e.CustomerType).HasName("PK__Customer__9881D5E1D0C5FFCE");

            entity.ToTable("CustomersType");

            entity.Property(e => e.CustomerType).ValueGeneratedNever();
            entity.Property(e => e.Desciption)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EngagedDetail>(entity =>
        {
            entity.HasKey(e => e.EngagedId).HasName("PK__EngagedD__39A60C78C5EB522E");

            entity.Property(e => e.EngagedId).HasColumnName("EngagedID");
            entity.Property(e => e.AdId).HasColumnName("AdID");
            entity.Property(e => e.CalaCity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CalaFatherName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CalaFirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CalaLastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChatanCity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChatanFatherName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChatanFirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChatanLastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateEngagement).HasColumnType("datetime");
            entity.Property(e => e.SeminarName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.YeshivaName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ad).WithMany(p => p.EngagedDetails)
                .HasForeignKey(d => d.AdId)
                .HasConstraintName("FK__EngagedDet__AdID__0F624AF8");
        });

        modelBuilder.Entity<GetUsersDetails1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetUsersDetails1");

            entity.Property(e => e.CharactersName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("charactersName");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.LastEnterDate)
                .HasColumnType("datetime")
                .HasColumnName("lastEnterDate");
            entity.Property(e => e.MainCommunication)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mainCommunication");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<GetUsersDetails2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetUsersDetails2");

            entity.Property(e => e.CharactersName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("charactersName");
            entity.Property(e => e.Contant)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contant");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.LastEnterDate)
                .HasColumnType("datetime")
                .HasColumnName("lastEnterDate");
            entity.Property(e => e.Passwords).HasColumnName("passwords");
            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<GetUsersDetails3>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetUsersDetails3");

            entity.Property(e => e.CharactersId).HasColumnName("charactersID");
            entity.Property(e => e.CharactersName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("charactersName");
            entity.Property(e => e.Contant)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contant");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.LastEnterDate)
                .HasColumnType("datetime")
                .HasColumnName("lastEnterDate");
            entity.Property(e => e.Passwords).HasColumnName("passwords");
            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<GetUsersDetails4>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetUsersDetails4");

            entity.Property(e => e.CharactersId).HasColumnName("charactersID");
            entity.Property(e => e.CharactersName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("charactersName");
            entity.Property(e => e.Contant)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contant");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.LastEnterDate)
                .HasColumnType("datetime")
                .HasColumnName("lastEnterDate");
            entity.Property(e => e.Passwords).HasColumnName("passwords");
            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<IshurForAd>(entity =>
        {
            entity.ToTable("IshurForAd");

            entity.Property(e => e.AdId).HasColumnName("adID");
            entity.Property(e => e.CharactersIdinsertRow).HasColumnName("charactersIDInsertRow");
            entity.Property(e => e.ExecutionDate)
                .HasColumnType("datetime")
                .HasColumnName("executionDate");
            entity.Property(e => e.KodIshur).HasColumnName("kodIshur");
            entity.Property(e => e.KodStatusIshur).HasColumnName("kodStatusIshur");
            entity.Property(e => e.Note)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.PublicationDateId).HasColumnName("PublicationDateID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Ad).WithMany(p => p.IshurForAds)
                .HasForeignKey(d => d.AdId)
                .HasConstraintName("FK__IshurForAd__adID__1EA48E88");

            entity.HasOne(d => d.CharactersIdinsertRowNavigation).WithMany(p => p.IshurForAds)
                .HasForeignKey(d => d.CharactersIdinsertRow)
                .HasConstraintName("FK__IshurForA__chara__2F9A1060");

            entity.HasOne(d => d.KodIshurNavigation).WithMany(p => p.IshurForAds)
                .HasForeignKey(d => d.KodIshur)
                .HasConstraintName("FK__IshurForA__kodIs__208CD6FA");

            entity.HasOne(d => d.KodStatusIshurNavigation).WithMany(p => p.IshurForAds)
                .HasForeignKey(d => d.KodStatusIshur)
                .HasConstraintName("FK__IshurForA__kodSt__3A4CA8FD");

            entity.HasOne(d => d.PublicationDate).WithMany(p => p.IshurForAds)
                .HasForeignKey(d => d.PublicationDateId)
                .HasConstraintName("FK__IshurForA__Publi__1F98B2C1");

            entity.HasOne(d => d.User).WithMany(p => p.IshurForAds)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__IshurForA__userI__2180FB33");
        });

        modelBuilder.Entity<Ishurim>(entity =>
        {
            entity.HasKey(e => e.KodIshur).HasName("PK__Ishurim__E459D42F812B4789");

            entity.ToTable("Ishurim");

            entity.Property(e => e.KodIshur).HasColumnName("kodIshur");
            entity.Property(e => e.CharactersId).HasColumnName("charactersID");
            entity.Property(e => e.CharactersIdinIshur).HasColumnName("charactersIDInIshur");
            entity.Property(e => e.CharactersIdinRejection).HasColumnName("charactersIDInRejection");
            entity.Property(e => e.CharactersIdinsertRow).HasColumnName("charactersIDInsertRow");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IshurDescribetion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.KodStatusInIshur).HasColumnName("kodStatusInIshur");
            entity.Property(e => e.KodStatusInRejection).HasColumnName("kodStatusInRejection");
            entity.Property(e => e.RejectionDescribetion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusAdId).HasColumnName("statusAdID");

            entity.HasOne(d => d.Characters).WithMany(p => p.IshurimCharacters)
                .HasForeignKey(d => d.CharactersId)
                .HasConstraintName("FK__Ishurim__charact__7F2BE32F");

            entity.HasOne(d => d.CharactersIdinIshurNavigation).WithMany(p => p.IshurimCharactersIdinIshurNavigations)
                .HasForeignKey(d => d.CharactersIdinIshur)
                .HasConstraintName("FK__Ishurim__charact__2DB1C7EE");

            entity.HasOne(d => d.CharactersIdinRejectionNavigation).WithMany(p => p.IshurimCharactersIdinRejectionNavigations)
                .HasForeignKey(d => d.CharactersIdinRejection)
                .HasConstraintName("FK__Ishurim__charact__2CBDA3B5");

            entity.HasOne(d => d.CharactersIdinsertRowNavigation).WithMany(p => p.IshurimCharactersIdinsertRowNavigations)
                .HasForeignKey(d => d.CharactersIdinsertRow)
                .HasConstraintName("FK__Ishurim__charact__2EA5EC27");

            entity.HasOne(d => d.KodStatusInIshurNavigation).WithMany(p => p.IshurimKodStatusInIshurNavigations)
                .HasForeignKey(d => d.KodStatusInIshur)
                .HasConstraintName("FK__Ishurim__kodStat__00200768");

            entity.HasOne(d => d.KodStatusInRejectionNavigation).WithMany(p => p.IshurimKodStatusInRejectionNavigations)
                .HasForeignKey(d => d.KodStatusInRejection)
                .HasConstraintName("FK__Ishurim__kodStat__01142BA1");

            entity.HasOne(d => d.StatusAd).WithMany(p => p.IshurimStatusAds)
                .HasForeignKey(d => d.StatusAdId)
                .HasConstraintName("FK__Ishurim__statusA__2057CCD0");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA4770229D514");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.DescriptionLocations)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PriceLocations).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A587A2FCDAC");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.ChargeAmount).HasColumnType("money");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Details)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Payments__Custom__282DF8C2");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("FK__Payments__Paymen__29221CFB");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Payments__UserID__2A164134");
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.PaymentStatus1).HasName("PK__PaymentS__143194FE618D03A6");

            entity.ToTable("PaymentStatus");

            entity.Property(e => e.PaymentStatus1).HasColumnName("PaymentStatus");
            entity.Property(e => e.Desciption)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentsType>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeId).HasName("PK__Payments__BA430B1583D1E08F");

            entity.ToTable("PaymentsType");

            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PriceList>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__PriceLis__4957584FC31B0A30");

            entity.ToTable("PriceList");

            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.AdTypeId).HasColumnName("AdTypeID");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.SectionId).HasColumnName("SectionID");
            entity.Property(e => e.SizeId).HasColumnName("SizeID");

            entity.HasOne(d => d.AdType).WithMany(p => p.PriceLists)
                .HasForeignKey(d => d.AdTypeId)
                .HasConstraintName("FK__PriceList__AdTyp__123EB7A3");

            entity.HasOne(d => d.Location).WithMany(p => p.PriceLists)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__PriceList__Locat__14270015");

            entity.HasOne(d => d.Section).WithMany(p => p.PriceLists)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK__PriceList__Secti__151B244E");

            entity.HasOne(d => d.Size).WithMany(p => p.PriceLists)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__PriceList__SizeI__1332DBDC");
        });

        modelBuilder.Entity<PublicationDate>(entity =>
        {
            entity.HasKey(e => e.PublicationDateId).HasName("PK__Publicat__0D94870628DA8AD5");

            entity.Property(e => e.PublicationDateId).HasColumnName("PublicationDateID");
            entity.Property(e => e.AdId).HasColumnName("AdID");
            entity.Property(e => e.PriceId).HasColumnName("PriceID");
            entity.Property(e => e.PublicationDate1)
                .HasColumnType("datetime")
                .HasColumnName("PublicationDate");
            entity.Property(e => e.StatusIshurId).HasColumnName("statusIshurId");

            entity.HasOne(d => d.Ad).WithMany(p => p.PublicationDates)
                .HasForeignKey(d => d.AdId)
                .HasConstraintName("FK__Publicatio__AdID__17F790F9");

            entity.HasOne(d => d.Price).WithMany(p => p.PublicationDates)
                .HasForeignKey(d => d.PriceId)
                .HasConstraintName("FK__Publicati__Price__18EBB532");

            entity.HasOne(d => d.StatusIshur).WithMany(p => p.PublicationDates)
                .HasForeignKey(d => d.StatusIshurId)
                .HasConstraintName("FK__Publicati__statu__1A9EF37A");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("PK__Sections__80EF0892DE14D7B8");

            entity.Property(e => e.SectionId).HasColumnName("SectionID");
            entity.Property(e => e.SectionName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ParentSectionNavigation).WithMany(p => p.InverseParentSectionNavigation)
                .HasForeignKey(d => d.ParentSection)
                .HasConstraintName("FK__Sections__Parent__403A8C7D");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizesId).HasName("PK__Sizes__E89D5F1F78E41358");

            entity.Property(e => e.SizesId).HasColumnName("SizesID");
            entity.Property(e => e.SizeDescriptionId).HasColumnName("SizeDescriptionID");

            entity.HasOne(d => d.SizeDescription).WithMany(p => p.Sizes)
                .HasForeignKey(d => d.SizeDescriptionId)
                .HasConstraintName("FK__Sizes__SizeDescr__4BAC3F29");
        });

        modelBuilder.Entity<SizesDescription>(entity =>
        {
            entity.HasKey(e => e.SizeDescriptionId).HasName("PK__SizesDes__FC442A78AC1BF0DE");

            entity.ToTable("SizesDescription");

            entity.Property(e => e.SizeDescriptionId).HasColumnName("SizeDescriptionID");
            entity.Property(e => e.DescriptionSizes)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PriceSize).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<StatusAd>(entity =>
        {
            entity.HasKey(e => e.StatusAdId).HasName("PK__StatusAd__11812EDCA1F434E9");

            entity.ToTable("StatusAd");

            entity.Property(e => e.StatusAdId).HasColumnName("statusAdID");
            entity.Property(e => e.StatusAdName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("statusAdName");
        });

        modelBuilder.Entity<StatusIshur>(entity =>
        {
            entity.HasKey(e => e.StatusIshurId).HasName("PK__StatusIs__04978EE32F2CE00B");

            entity.ToTable("StatusIshur");

            entity.Property(e => e.StatusIshurId).HasColumnName("statusIshurID");
            entity.Property(e => e.StatusIshurName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("statusIshurName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CDFAFCE08EB");

            entity.HasIndex(e => e.Passwords, "UQ__Users__21178534A3102621").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.CharactersId).HasColumnName("charactersID");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.LastEnterDate)
                .HasColumnType("datetime")
                .HasColumnName("lastEnterDate");
            entity.Property(e => e.Passwords).HasColumnName("passwords");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("userName");

            entity.HasOne(d => d.Characters).WithMany(p => p.Users)
                .HasForeignKey(d => d.CharactersId)
                .HasConstraintName("FK__Users__character__04E4BC85");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    public async Task<int> ExecuteSqlRawAsync(string sqlQuery, SqlParameter[] parameters)
    {
        return await Database.ExecuteSqlRawAsync(sqlQuery, parameters);
    }
    public int ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
    {
        var sqlQuery = $"EXEC {storedProcedureName} ";
        sqlQuery += string.Join(", ", parameters.Select(p => $"@{p.ParameterName} = @{p.ParameterName}"));

        return  Database.ExecuteSqlRaw(sqlQuery, parameters);
    }
}
