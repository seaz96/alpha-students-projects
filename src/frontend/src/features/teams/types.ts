export interface ITeam {
  id: string;
  name: string;
  description: string;
  projectId: string;
  teamprojectLink: string;
  students: IStudent[];
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
  id: string;
  firstName: string;
  middleName: string;
  lastName: string;
  academicGroup: string;
  email: string;
  phone: string;
  position: IStudentPosition;
  telegram: string;
}

export type ICreateStudentArgs = Omit<IStudent, "position" | "id"> & {
  positionId: string;
};

export type IPatchStudentArgs = Omit<IStudent, "position"> & {
  teamId: string;
  positionId: string;
};

export interface IStudentPosition {
  id: string;
  name: string;
}
