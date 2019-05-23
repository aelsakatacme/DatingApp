import { PhotoDTO } from './PhotoDTO';

export class UserDTO {
 id: number;
 fullName: string;
 username: string;
 password: string;

 knownAs?: string;

 genderId?: number;
 genderName: string;

 cityId?: number;
 cityName: string;
 // city?: any;

 countryId?: number;
 countryName: string;
 // country?: any;

 dateOfBirth?: Date;
 lastActive?: Date;
 createdOn?: Date;
 age?: number;
 fullAge?: string;
 photoUrl?: string;

 introduction: string;
 lookingFor: string;
 interests: string;

 photos?: PhotoDTO[];

 public UserDTO() { }
}


