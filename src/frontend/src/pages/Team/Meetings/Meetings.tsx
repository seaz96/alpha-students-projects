import { useGetMeetingsQuery } from "@/features/meetings/meetingsApi";
import MeetingsList from "./MeetingsList";
import { useMemo } from "react";
import { Spinner } from "@/components/ui/spinner";

export default function Meetings({ teamId }: { teamId: string }) {
  const queryParams = useMemo(
    () => ({
      teamId,
      startDate: new Date(0).toISOString(),
      endDate: new Date(2000000000000).toISOString(),
    }),
    [teamId],
  );

  const { data: meetings, isLoading } = useGetMeetingsQuery(queryParams);

  if (isLoading && !meetings)
    return (
      <p className="flex items-center gap-1">
        <Spinner /> Загрузка...
      </p>
    );

  return (
    <div>
      <MeetingsList data={meetings} />
    </div>
  );
}
