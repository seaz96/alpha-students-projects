export interface IStage {
  startDate: string;
  endDate: string;
  id: string;
  mentorComment: string | null;
  mentorScore: number | null;
  name: string;
  teamId: string;
  urfuComment: string | null;
  urfuScore: number | null;
}

export interface PostStageArgs {
  endDate: string;
  startDate: string;
  name: string;
  teamId: string;
}

export interface PatchStageArgs {
  stageId: string;
  startDate: string;
  endDate: string;
  mentorComment: string | null;
  mentorScore: number | null;
  urfuComment: string | null;
  urfuScore: number | null;
  name: string;
}
