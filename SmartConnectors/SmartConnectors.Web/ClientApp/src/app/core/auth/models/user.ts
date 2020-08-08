export interface organization {
  orgId: string;
  orgName: string;
  orgType: string;
  roles: string[];
}
export interface UserProfile {
  name: string;
  lastLogin: string;
  organizations: organization[];
  userId: number;
}


export class User implements UserProfile {
  userId: number;
  name: string;
  lastLogin: string;
  organizations: organization[];

  constructor(json: UserProfile) {
    this.name = json.name || '';
    this.lastLogin = json.lastLogin || null;
    this.organizations = json.organizations || null;
    this.userId = json.userId || null;
  }

  static fromJSON(json) {
    return new User(json);
  }
}

export interface UserStatus {
  status: boolean,
  showModal: boolean
}