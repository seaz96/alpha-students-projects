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
import { useState } from "react";

export default function ApplicationReviewsPopover({
  likes,
  id,
}: {
  likes: number;
  id: string;
}) {
  const [isOpen, setIsOpen] = useState(false);
  const { data: reviews, isLoading } = useGetReviewsQuery(
    { id, limit: 99, offset: 0 },
    { skip: !isOpen },
  );

  return (
    <Popover open={isOpen} onOpenChange={setIsOpen}>
      <PopoverTrigger asChild>
        <Button variant="outline" size="icon-sm">
          {likes}
        </Button>
      </PopoverTrigger>
      <PopoverContent className="space-y-2">
        {isLoading && <Spinner />}

        {!isLoading && reviews && reviews.data.length > 0 ? (
          reviews.data.map((r, i) => (
            <div key={i} className="text-sm">
              <div className="flex">
                {r.isDislike ? (
                  <ArrowDownIcon className="relative top-0.5 size-4" />
                ) : (
                  <ArrowUpIcon className="relative top-0.5 size-4" />
                )}
                <p className="font-medium">{getInitials(r.author)}</p>
              </div>
              <p>{r.comment}</p>
            </div>
          ))
        ) : (
          <p className="text-sm">Нет отзывов</p>
        )}
      </PopoverContent>
    </Popover>
  );
}
