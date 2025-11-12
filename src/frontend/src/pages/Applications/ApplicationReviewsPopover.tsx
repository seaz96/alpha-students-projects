import { Button } from "@/components/ui/button";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Spinner } from "@/components/ui/spinner";
import { useGetReviewsQuery } from "@/features/cases/casesApi";
import { getInitials } from "@/lib/utils";
import { ArrowDownIcon, ArrowUpIcon } from "lucide-react";
import { useEffect } from "react";

export default function ApplicationReviewsPopover({
  likes,
  id,
}: {
  likes: number;
  id: string;
}) {
  const { data, isLoading } = useGetReviewsQuery({ id, limit: 99, offset: 0 });

  useEffect(() => {
    console.log(data);
  }, [data]);

  if (isLoading || data === undefined)
    return (
      <Button disabled variant="ghost" size="icon-sm">
        <Spinner />
      </Button>
    );

  return (
    <Popover>
      <PopoverTrigger asChild>
        <Button variant="ghost" size="icon-sm">
          {likes}
        </Button>
      </PopoverTrigger>
      <PopoverContent className="space-y-2">
        {data.length === 0 ? (
          <p>Нет отзывов</p>
        ) : (
          data.map((r, i) => (
            <div key={i} className="text-sm">
              <div className="flex">
                {r.isDislike ? (
                  <ArrowDownIcon className="relative top-0.5 size-4" />
                ) : (
                  <ArrowUpIcon className="relative top-0.5 size-4" />
                )}{" "}
                <p className="font-medium">{getInitials(r.author)}</p>
              </div>
              <p>{r.comment}</p>
            </div>
          ))
        )}
      </PopoverContent>
    </Popover>
  );
}
