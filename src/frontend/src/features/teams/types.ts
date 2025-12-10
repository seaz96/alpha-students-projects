export interface ITeam {
  id: string;
  name: string;
  description: string;
  projectId: string;
  teamprojectLink: string;
}

export interface ICreateTeamArgs {
  name: string;
  description: string;
  projectId: string;
  teamprojectLink: string;
}

export interface IPatchTeamArgs {
  name: string;
  description: string;
  teamId: string;
  teamprojectLink: string;
}

export interface IStudent {
  firstName: string;
  middleName: string;
  lastName: string;
  academicalGroup: string;
  email: string;
  phone: string;
  positionId: string | null;
  telegram: string;
}
