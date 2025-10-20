using static finalProject.COMMON.Enums;

namespace finalProject
{
    public static class Helper
    {
        public static NextOrReverseStatusAndRoll GetNextStatus(AdStatusEnum cuurentAdStatus, bool isExistsPublicationDates)
        {
            NextOrReverseStatusAndRoll norsar;
            switch (cuurentAdStatus)
            {
                case AdStatusEnum.Bid:
                    //return AdStatusEnum.CustomerMaterials;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.CustomerMaterials, RolesEnum.secretary);
                case AdStatusEnum.CustomerMaterials:
                    //return AdStatusEnum.Graphics;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.Graphics, RolesEnum.secretary);
                case AdStatusEnum.Graphics:
                    //return AdStatusEnum.CustomerApprovalGraphics;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.CustomerApprovalGraphics, RolesEnum.graphicArtist);
                case AdStatusEnum.CustomerApprovalGraphics:
                    //return AdStatusEnum.Paging;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.Paging, RolesEnum.secretary);
                case AdStatusEnum.Paging:
                    //return AdStatusEnum.Publication;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.WillPublication, RolesEnum.pagination);
                case AdStatusEnum.WillPublication:
                    //return isExistsPublicationDates ? AdStatusEnum.Paging : AdStatusEnum.Payment;
                    return isExistsPublicationDates ?  new NextOrReverseStatusAndRoll(AdStatusEnum.Paging, RolesEnum.secretary) : new NextOrReverseStatusAndRoll(AdStatusEnum.Publicated, RolesEnum.secretary);
                case AdStatusEnum.Payment:
                    //return AdStatusEnum.PaidUp;
                    return  new NextOrReverseStatusAndRoll(AdStatusEnum.PaidUp, RolesEnum.secretary);
                default: 
                    //return AdStatusEnum.Cancelled;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.Cancelled, RolesEnum.secretary);
            }
        }
        public static NextOrReverseStatusAndRoll GetReversStatus(AdStatusEnum cuurentAdStatus)
        {
            NextOrReverseStatusAndRoll norsar;
            switch (cuurentAdStatus)
            {
                case AdStatusEnum.Bid:
                    //return AdStatusEnum.Cancelled;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.Cancelled, RolesEnum.secretary);
                case AdStatusEnum.CustomerMaterials:
                    //return AdStatusEnum.Cancelled;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.Cancelled, RolesEnum.secretary);
                case AdStatusEnum.Graphics:
                    //return AdStatusEnum.CustomerMaterials;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.CustomerMaterials, RolesEnum.graphicArtist);
                case AdStatusEnum.CustomerApprovalGraphics:
                    //return AdStatusEnum.Graphics;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.Graphics, RolesEnum.secretary);
                case AdStatusEnum.Paging:
                    //return AdStatusEnum.CustomerApprovalGraphics;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.CustomerApprovalGraphics, RolesEnum.pagination);
                case AdStatusEnum.WillPublication:
                    //return AdStatusEnum.Cancelled;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.Cancelled, RolesEnum.secretary);
                default:
                    //return AdStatusEnum.Cancelled;
                    return new NextOrReverseStatusAndRoll(AdStatusEnum.Cancelled, RolesEnum.secretary);
            }

        }
        public class NextOrReverseStatusAndRoll
        {
            public NextOrReverseStatusAndRoll(AdStatusEnum ase, RolesEnum re) 
            { 
                this.RolesEnum = re;
                this.AdStatusEnum = ase;
            }
             public RolesEnum RolesEnum { get; set; }
             public AdStatusEnum AdStatusEnum { get; set; }
        }
        public static RolesEnum CharactersNameToEnum(string charactersName)
        {
            switch (charactersName)
            {
                case "מנהל":
                    return RolesEnum.manager;
                case "מזכירה":
                    return RolesEnum.secretary;
                case "מעמד":
                    return RolesEnum.pagination;
                case "גרפיקאית":
                    return RolesEnum.graphicArtist;
                default: return RolesEnum.secretary;
            }
        }
    }
}
