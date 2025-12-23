import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import { DataTable } from "@/components/ui/data-table";
import {
  Dialog,
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
import { Spinner } from "@/components/ui/spinner";
import { useCreateMeetingMutation } from "@/features/meetings/meetingsApi";
import type { IMeeting } from "@/features/meetings/types";
import { cn } from "@/lib/utils";
import type { GetResponse } from "@/types";
import { zodResolver } from "@hookform/resolvers/zod";
import type { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { ru } from "date-fns/locale";
import { CalendarIcon, ExternalLinkIcon, PlusIcon } from "lucide-react";
import { useCallback, useMemo, useState } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router";
import { Link } from "react-router";
import z from "zod";
import MeetingRowActions from "./MeetingRowActions";
import Todos from "./Todos";

const createSchema = z.object({
  name: z.string().min(1, "Обязательное поле"),
  date: z.date("Выберите дату"),
});

export default function MeetingsList({
  data,
}: {
  data: GetResponse<IMeeting> | undefined;
}) {
  const { teamId } = useParams();
  const [createMeeting] = useCreateMeetingMutation();
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const columns = useMemo<ColumnDef<IMeeting>[]>(
    () => [
      {
        accessorKey: "name",
        header: "Название",
        cell: ({ row }) => <p className="font-medium">{row.original.name}</p>,
      },
      {
        accessorKey: "date",
        header: "Дата",
        cell: ({ row }) => (
          <p>{format(new Date(row.original.date), "PPP", { locale: ru })}</p>
        ),
      },
      {
        accessorKey: "score",
        header: "Оценка",
        cell: ({ row }) => {
          return (
            <div className="flex items-center gap-2">
              <span>{row.original.score}</span>
            </div>
          );
        },
      },
      {
        accessorKey: "Итоги",
        header: "Итоги",
        cell: ({ row }) => (
          <p className="max-w-64 truncate">{row.original.summary}</p>
        ),
      },
      {
        accessorKey: "todos",
        header: "Задачи",
        cell: ({ row }) => <Todos meeting={row.original} />,
      },
      {
        accessorKey: "recordList",
        header: "Запись",
        cell: ({ row }) => {
          const link = row.original.recordLink;
          if (!link) return <span className="text-muted-foreground">-</span>;
          return (
            <Button asChild variant="outline" size="icon-sm">
              <Link
                to={link}
                target="_blank"
                rel="noreferrer"
                className="flex items-center gap-1 text-blue-600 hover:underline"
              >
                <ExternalLinkIcon className="size-3" />
              </Link>
            </Button>
          );
        },
      },
      {
        id: "actions",
        header: "",
        cell: ({ row }) => (
          <div>
            <MeetingRowActions meeting={row.original} />
          </div>
        ),
      },
    ],
    [],
  );

  const form = useForm<z.infer<typeof createSchema>>({
    resolver: zodResolver(createSchema),
    defaultValues: {
      name: "",
      date: new Date(),
    },
  });

  const onSubmit = useCallback(
    async (values: z.infer<typeof createSchema>) => {
      if (!teamId) return;
      await createMeeting({
        ...values,
        date: values.date.toISOString(),
        teamId,
      });
      form.reset();
      setIsDialogOpen(false);
    },
    [createMeeting, form, teamId],
  );

  if (data === undefined) return <Spinner />;
  const { data: meetings } = data;

  return (
    <div className="flex flex-col gap-1">
      <div className="flex justify-start gap-1">
        <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
          <DialogTrigger asChild>
            <Button>
              <PlusIcon className="mr-2 size-4" />
              Создать встречу
            </Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Создать встречу</DialogTitle>
              <DialogDescription />
            </DialogHeader>
            <Form {...form}>
              <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="space-y-4"
              >
                <FormField
                  control={form.control}
                  name="name"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Название</FormLabel>
                      <FormControl>
                        <Input placeholder="Еженедельный синк..." {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="date"
                  render={({ field }) => (
                    <FormItem className="flex flex-col">
                      <FormLabel>Дата проведения</FormLabel>
                      <Popover>
                        <PopoverTrigger asChild>
                          <FormControl>
                            <Button
                              variant={"outline"}
                              className={cn(
                                "w-full pl-3 text-left font-normal",
                                !field.value && "text-muted-foreground",
                              )}
                            >
                              {field.value ? (
                                format(field.value, "PPP", { locale: ru })
                              ) : (
                                <span>Выберите дату</span>
                              )}
                              <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                            </Button>
                          </FormControl>
                        </PopoverTrigger>
                        <PopoverContent className="w-auto p-0" align="start">
                          <Calendar
                            mode="single"
                            selected={field.value}
                            onSelect={field.onChange}
                            disabled={(date) => date < new Date("1900-01-01")}
                            locale={ru}
                          />
                        </PopoverContent>
                      </Popover>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <DialogFooter>
                  <Button type="submit">Создать</Button>
                </DialogFooter>
              </form>
            </Form>
          </DialogContent>
        </Dialog>

        <Input placeholder="Поиск..." className="w-fit max-w-sm" />
      </div>

      <DataTable columns={columns} data={meetings} />
    </div>
  );
}
