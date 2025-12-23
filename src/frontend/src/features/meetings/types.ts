export interface IMeeting {
  id: string;
  name: string;
  date: string;
  summary: string;
  recordLink: string;
  score: number;
  nextId: string;
  previousId: string;
}

export interface IPostMeeting {
  date: string;
  name: string;
  teamId: string;
}

export interface IPatchMeeting {
  meetingId: string;

  date: string;
  name: string;
  recordLink: string;
  score: number;
  summary: string;
}
