import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import {
  useCreateStageMutation,
  useGetStagesQuery,
} from "@/features/stages/stagesApi";
import { cn } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import { format } from "date-fns";
import { ru } from "date-fns/locale";
import { CalendarIcon } from "lucide-react";
import { useCallback, useState } from "react";
import { useForm } from "react-hook-form";
import z from "zod";
import StagesDataTable from "./StagesDataTable";

const schema = z.object({
  name: z.string().min(1, "Обязательное поле"),
  dateRange: z
    .object(
      {
        from: z.date(),
        to: z.date(),
      },
      "Выберите период проведения",
    )
    .refine((data) => !!data.from && !!data.to, {
      message: "Необходимо выбрать и дату начала, и дату окончания",
    }),
});

export default function Stages({ teamId }: { teamId: string }) {
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const { data: stages } = useGetStagesQuery({
    teamId,
    limit: 99999,
    offset: 0,
  });

  const [createStage] = useCreateStageMutation();

  const form = useForm<z.infer<typeof schema>>({
    resolver: zodResolver(schema),
    defaultValues: {
      name: "",
      dateRange: {
        from: new Date(),
        to: new Date(Date.now() + 14 * 24 * 3600 * 1000),
      },
    },
  });

  const onSubmit = useCallback(
    async (values: z.infer<typeof schema>) => {
      await createStage({
        teamId,
        name: values.name,
        startDate: values.dateRange.from.toISOString(),
        endDate: values.dateRange.to.toISOString(),
      });
      setIsDialogOpen(false);
      form.reset();
    },
    [createStage, form, teamId],
  );

  if (!teamId) return null;

  return (
    <div className="mt-4 flex flex-col gap-1">
      <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
        <DialogTrigger asChild>
          <Button className="w-fit">
            <CalendarIcon className="mr-2 h-4 w-4" />
            Создать этап
          </Button>
        </DialogTrigger>
        <DialogContent className="max-w-[600px]">
          <DialogHeader>
            <DialogTitle>Создать новый этап</DialogTitle>
            <DialogDescription>
              Заполните данные для создания нового этапа работы.
            </DialogDescription>
          </DialogHeader>

          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Название</FormLabel>
                    <FormControl>
                      <Input {...field} placeholder="Например: Дизайн" />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />

              <FormField
                control={form.control}
                name="dateRange"
                render={({ field }) => (
                  <FormItem className="flex flex-col">
                    <FormLabel>Период проведения</FormLabel>
                    <Popover>
                      <PopoverTrigger asChild>
                        <FormControl>
                          <Button
                            id="date"
                            variant={"outline"}
                            className={cn(
                              "w-full justify-start text-left font-normal",
                              !field.value && "text-muted-foreground",
                            )}
                          >
                            <CalendarIcon className="mr-2 h-4 w-4" />
                            {field.value?.from ? (
                              field.value.to ? (
                                <>
                                  {format(field.value.from, "dd MMM y", {
                                    locale: ru,
                                  })}{" "}
                                  -{" "}
                                  {format(field.value.to, "dd MMM y", {
                                    locale: ru,
                                  })}
                                </>
                              ) : (
                                format(field.value.from, "dd MMM y", {
                                  locale: ru,
                                })
                              )
                            ) : (
                              <span>Выберите даты</span>
                            )}
                          </Button>
                        </FormControl>
                      </PopoverTrigger>
                      <PopoverContent className="w-auto p-0" align="start">
                        <Calendar
                          mode="range"
                          defaultMonth={field.value?.from}
                          selected={field.value}
                          onSelect={field.onChange}
                          locale={ru}
                        />
                      </PopoverContent>
                    </Popover>
                    <FormMessage />
                  </FormItem>
                )}
              />

              <DialogFooter className="mt-6">
                <DialogClose asChild>
                  <Button variant="secondary" size="sm" type="button">
                    Отмена
                  </Button>
                </DialogClose>
                <Button size="sm" type="submit">
                  Создать
                </Button>
              </DialogFooter>
            </form>
          </Form>
        </DialogContent>
      </Dialog>

      {stages && <StagesDataTable stages={stages.data} />}
    </div>
  );
}
