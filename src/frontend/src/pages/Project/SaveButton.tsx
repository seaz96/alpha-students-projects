import { Button } from "@/components/ui/button";
import { SaveIcon } from "lucide-react";
import type { ComponentProps } from "react";

export default function SaveButton({
  className,
  ...rest
}: ComponentProps<"button">) {
  return (
    <Button size="sm" variant="outline" className={className} {...rest}>
      <SaveIcon />
      Сохранить
    </Button>
  );
}
