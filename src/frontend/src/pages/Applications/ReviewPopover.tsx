import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Textarea } from "@/components/ui/textarea";
import { useReviewCaseMutation } from "@/features/cases/casesApi";
import { PopoverClose } from "@radix-ui/react-popover";
import { useState } from "react";

interface ReviewPopoverProps {
  caseId: string;
  isDislike: boolean;
  children: React.ReactNode;
}

export default function ReviewPopover({
  caseId,
  isDislike,
  children,
}: ReviewPopoverProps) {
  const [comment, setComment] = useState("");
  const [reviewCase] = useReviewCaseMutation();

  const handleReview = async () => {
    await reviewCase({
      id: caseId,
      review: {
        comment,
        isDislike,
      },
    });
    setComment("");
  };

  return (
    <Popover>
      <PopoverTrigger asChild>{children}</PopoverTrigger>
      <PopoverContent className="flex w-80 flex-col gap-2">
        <Label>Комментарий</Label>
        <Textarea
          value={comment}
          onChange={(e) => setComment(e.target.value)}
        />
        <PopoverClose asChild>
          <Button className="ml-auto" onClick={handleReview}>
            Отправить
          </Button>
        </PopoverClose>
      </PopoverContent>
    </Popover>
  );
}
