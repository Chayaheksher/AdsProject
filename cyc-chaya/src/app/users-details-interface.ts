import { Communication } from "./edit-or-new/edit-or-new.component";

export interface UsersDetailsInterface {
  userId: number;
  charactersId: number;
  lastEnterDate: Date;
  fullName: string;
  userName: string;
  passwords: number;
  charactersName: string;
  communicationUsers: {
    communicationTypeDescription: string;
    communicationID: number;
    communicationDetails: string;
    mainComunication: boolean;
  }[];
}
export interface UsersDetailsInterface1 {
  userId: number;
  charactersId: number;
  fullName: string;
  userName: string;
  passwords: number;
  communicationUsers: {
    communicationID: number;
    communicationTypeDescription: string;
    communicationDetails: string;
    mainComunication: boolean;
  }[];
}