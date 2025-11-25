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
