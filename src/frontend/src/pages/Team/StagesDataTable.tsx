import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import { DataTable } from "@/components/ui/data-table";
import {
  Dialog,
  DialogContent,
  DialogFooter,
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
  useDeleteStageMutation,
  usePatchStageMutation,
} from "@/features/stages/stagesApi";
import type { IStage } from "@/features/stages/types";
import { cn } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import type { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { ru } from "date-fns/locale";
import { CalendarIcon, Edit2Icon, TrashIcon } from "lucide-react";
import { useMemo, useState } from "react";
import { useForm } from "react-hook-form";
import z from "zod";

const patchSchema = z.object({
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
  mentorScore: z
    .number()
    .min(0, "Число от 0 до 100")
    .max(100, "Число от 0 до 100")
    .optional(),
  mentorComment: z.string().optional(),
  urfuScore: z
    .number()
    .min(0, "Число от 0 до 100")
    .max(100, "Число от 0 до 100")
    .optional(),
  urfuComment: z.string().optional(),
});

export default function StagesDataTable({ stages }: { stages: IStage[] }) {
  const columns = useMemo<ColumnDef<IStage>[]>(
    () => [
      {
        accessorKey: "name",
        header: "Этап",
        cell: ({ row }) => <p>{row.original.name}</p>,
      },
      {
        accessorKey: "startDate",
        header: "Дата начала",
        cell: ({ row }) => (
          <p>{new Date(row.original.startDate).toLocaleDateString("ru-RU")}</p>
        ),
      },
      {
        accessorKey: "endDate",
        header: "Дата окончания",
        cell: ({ row }) => (
          <p>{new Date(row.original.endDate).toLocaleDateString("ru-RU")}</p>
        ),
      },
      {
        accessorKey: "mentorScore",
        header: "Оценка ментора",
        cell: ({
          row: {
            original: { mentorScore, mentorComment },
          },
        }) =>
          mentorComment ? (
            <Popover>
              <PopoverTrigger>
                <p className="underline underline-offset-2">
                  {mentorScore !== null ? mentorScore : "−"}
                </p>
              </PopoverTrigger>
              <PopoverContent>
                <p className="text-sm">{mentorComment}</p>
              </PopoverContent>
            </Popover>
          ) : (
            <p>{mentorScore !== null ? mentorScore : "−"}</p>
          ),
      },
      {
        accessorKey: "urfuScore",
        header: "Оценка университета",
        cell: ({
          row: {
            original: { urfuScore, urfuComment },
          },
        }) =>
          urfuComment ? (
            <Popover>
              <PopoverTrigger>
                <p className="underline underline-offset-2">
                  {urfuScore !== null ? urfuScore : "−"}
                </p>
              </PopoverTrigger>
              <PopoverContent>
                <p className="text-sm">{urfuComment}</p>
              </PopoverContent>
            </Popover>
          ) : (
            <p>{urfuScore !== null ? urfuScore : "−"}</p>
          ),
      },
      {
        accessorKey: "controls",
        header: "",
        cell: ({ row }) => <StageRowActions stage={row.original} />,
      },
    ],
    [],
  );

  return (
    <div>
      <DataTable columns={columns} data={stages} />
    </div>
  );
}

function StageRowActions({ stage }: { stage: IStage }) {
  const [deleteStage] = useDeleteStageMutation();
  const [patchStage] = usePatchStageMutation();
  const [isOpen, setIsOpen] = useState(false);

  const form = useForm<z.infer<typeof patchSchema>>({
    resolver: zodResolver(patchSchema),
    defaultValues: {
      name: stage.name,
      dateRange: {
        from: new Date(stage.startDate),
        to: new Date(stage.endDate),
      },
      mentorComment: stage.mentorComment || "",
      mentorScore: stage.mentorScore || 0,
      urfuComment: stage.urfuComment || "",
      urfuScore: stage.urfuScore || 0,
    },
  });

  function onSubmit(values: z.infer<typeof patchSchema>) {
    patchStage({
      stageId: stage.id,
      name: values.name,
      mentorComment: values.mentorComment || null,
      mentorScore: values.mentorScore || null,
      urfuComment: values.urfuComment || null,
      urfuScore: values.urfuScore || null,
      startDate: values.dateRange.from.toISOString(),
      endDate: values.dateRange.to.toISOString(),
    });
    setIsOpen(false);
  }

  return (
    <div className="flex gap-1">
      <Dialog open={isOpen} onOpenChange={setIsOpen}>
        <DialogTrigger asChild>
          <Button size="icon-sm" variant="outline">
            <Edit2Icon className="size-4" />
          </Button>
        </DialogTrigger>
        <DialogContent className="max-w-[600px]">
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Название</FormLabel>
                    <FormControl>
                      <Input {...field} placeholder="Название этапа" />
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
                    <FormLabel>Период этапа</FormLabel>
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

              <div className="space-y-2 rounded-md border p-3">
                <h4 className="text-muted-foreground mb-2 text-sm font-medium">
                  Ментор
                </h4>
                <div className="grid grid-cols-4 gap-4">
                  <FormField
                    control={form.control}
                    name="mentorScore"
                    render={({ field }) => (
                      <FormItem className="col-span-1">
                        <FormLabel>Оценка</FormLabel>
                        <FormControl>
                          <Input
                            type="number"
                            min={0}
                            max={100}
                            {...field}
                            onChange={(e) =>
                              field.onChange(e.target.valueAsNumber)
                            }
                          />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="mentorComment"
                    render={({ field }) => (
                      <FormItem className="col-span-3">
                        <FormLabel>Комментарий</FormLabel>
                        <FormControl>
                          <Input
                            {...field}
                            placeholder="Комментарий ментора..."
                          />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                </div>
              </div>

              <div className="space-y-2 rounded-md border p-3">
                <h4 className="text-muted-foreground mb-2 text-sm font-medium">
                  Университет
                </h4>
                <div className="grid grid-cols-4 gap-4">
                  <FormField
                    control={form.control}
                    name="urfuScore"
                    render={({ field }) => (
                      <FormItem className="col-span-1">
                        <FormLabel>Оценка</FormLabel>
                        <FormControl>
                          <Input
                            type="number"
                            min={0}
                            max={100}
                            {...field}
                            onChange={(e) =>
                              field.onChange(e.target.valueAsNumber)
                            }
                          />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="urfuComment"
                    render={({ field }) => (
                      <FormItem className="col-span-3">
                        <FormLabel>Комментарий</FormLabel>
                        <FormControl>
                          <Input
                            {...field}
                            placeholder="Комментарий университета..."
                          />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                </div>
              </div>

              <DialogFooter>
                <Button type="submit">Сохранить</Button>
              </DialogFooter>
            </form>
          </Form>
        </DialogContent>
      </Dialog>

      <Button
        size="icon-sm"
        variant="outline"
        onClick={() => deleteStage({ id: stage.id })}
      >
        <TrashIcon className="size-4" />
      </Button>
    </div>
  );
}
