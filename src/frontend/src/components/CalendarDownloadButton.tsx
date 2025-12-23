import { Button } from "@/components/ui/button";
import {
  downloadMentorCalendar,
  downloadTeamCalendar,
} from "@/features/calendar/calendarActions";
import { cn } from "@/lib/utils";
import { CalendarIcon, Loader2Icon } from "lucide-react";
import { useState } from "react";
import { toast } from "sonner";

type CalendarDownloadButtonProps = {
  className?: string;
} & ({ type: "team"; teamId: string } | { type: "mentor"; mentorId?: string });

export default function CalendarDownloadButton(
  props: CalendarDownloadButtonProps,
) {
  const [isDownloading, setIsDownloading] = useState(false);
  const { className, type } = props;

  const handleDownload = async () => {
    setIsDownloading(true);
    try {
      if (type === "team") {
        await downloadTeamCalendar(props.teamId);
      } else {
        await downloadMentorCalendar(props.mentorId);
      }
      toast.success("Скачивание началось");
    } catch (error) {
      console.error(error);
      toast.error("Не удалось скачать файл календаря.");
    } finally {
      setIsDownloading(false);
    }
  };

  return (
    <Button
      variant="outline"
      onClick={handleDownload}
      disabled={isDownloading}
      className={cn("mb-2", className)}
    >
      {isDownloading ? (
        <Loader2Icon className="mr-2 h-4 w-4 animate-spin" />
      ) : (
        <CalendarIcon className="mr-2 h-4 w-4" />
      )}
      Скачать календарь
    </Button>
  );
}
