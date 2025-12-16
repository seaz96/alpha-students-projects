import { Button } from "@/components/ui/button";
import { DataTable } from "@/components/ui/data-table";
import {
  Dialog,
  DialogContent,
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
import { useAddStudentMutation } from "@/features/teams/teamsApi";
import type { IStudent } from "@/features/teams/types";
import { zodResolver } from "@hookform/resolvers/zod";
import type { ColumnDef } from "@tanstack/react-table";
import { User2Icon } from "lucide-react";
import { useCallback, useMemo, useState } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router";
import z from "zod";

export function StudentsDataTable() {
  const columns = useMemo<ColumnDef<IStudent & { id: string }>[]>(
    () => [
      {
        accessorKey: "name",
        header: "Студент",
        cell: ({ row }) => (
          <p>
            {row.original.middleName} {row.original.firstName}{" "}
            {row.original.lastName}
          </p>
        ),
      },
      {
        accessorKey: "academicalGroup",
        header: "Группа",
        cell: ({ row }) => <p>{row.original.academicalGroup}</p>,
      },
      {
        accessorKey: "email",
        header: "Почта",
        cell: ({ row }) => <p>{row.original.email}</p>,
      },
      {
        accessorKey: "phone",
        header: "Телефон",
        cell: ({ row }) => <p>{row.original.phone}</p>,
      },
      {
        accessorKey: "telegram",
        header: "Telegram",
        cell: ({ row }) => <p>{row.original.telegram}</p>,
      },
    ],
    [],
  );

  const { teamId } = useParams();
  const [addStudent] = useAddStudentMutation();

  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const schema = z.object({
    firstName: z.string().min(1, "Обязательное поле").trim(),
    middleName: z.string().min(1, "Обязательное поле").trim(),
    lastName: z.string().min(1, "Обязательное поле").trim(),
    academicalGroup: z
      .string("Обязательное поле")
      .regex(/^[А-ЯЁ]{1,5}-\d{6}$/, "Неверный формат группы"),
    email: z.email("Неправильный формат почты"),
    phone: z
      .string("Обязательное поле")
      .transform((val) => val.replace(/[^\d+]/g, ""))
      .refine((val) => /^(\+7|8|7)\d{10}$/.test(val), {
        message: "Некорректный номер телефона",
      }),
    telegram: z
      .string("Обязательное поле")
      .trim()
      .transform((val) => (val.startsWith("@") ? val.slice(1) : val))
      .pipe(
        z
          .string()
          .min(5, "Минимум 5 символов")
          .max(32, "Максимум 32 символа")
          .regex(/^[a-zA-Z][a-zA-Z0-9_]*$/, "Некорректное имя пользователя"),
      ),
  });

  const form = useForm<z.infer<typeof schema>>({
    resolver: zodResolver(schema),
    defaultValues: {
      academicalGroup: "",
      email: "",
      firstName: "",
      lastName: "",
      middleName: "",
      phone: "",
      telegram: "",
    },
  });

  const onSubmit = useCallback(
    async (values: z.infer<typeof schema>) => {
      await addStudent({ ...values, teamId: teamId!, positionId: null });
      form.reset();
      setIsDialogOpen(false);
    },
    [addStudent, form, teamId],
  );

  const mockData: (IStudent & { id: string })[] = [
    {
      id: "0",
      academicalGroup: "РИ-420941",
      firstName: "Никита",
      middleName: "Лещенко",
      lastName: "Александрович",
      telegram: "qkeeper",
      email: "example@gmail.com",
      phone: "+7 (904) 467 26 03",
      positionId: null,
    },
  ];

  return (
    <div className="mt-4 flex flex-col gap-1">
      <div className="flex gap-1">
        <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
          <DialogTrigger asChild>
            <Button>
              <User2Icon className="size-4" />
              Добавить студента
            </Button>
          </DialogTrigger>
          <DialogContent aria-describedby="">
            <DialogHeader>
              <DialogTitle>Добавить студента</DialogTitle>
            </DialogHeader>
            <Form {...form}>
              <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="space-y-2"
              >
                <div className="grid grid-cols-3 gap-2">
                  <FormField
                    control={form.control}
                    name="middleName"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Фамилия</FormLabel>
                        <FormControl>
                          <Input {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="firstName"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Имя</FormLabel>
                        <FormControl>
                          <Input {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                  <FormField
                    control={form.control}
                    name="lastName"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Отчество</FormLabel>
                        <FormControl>
                          <Input {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                </div>
                <FormField
                  control={form.control}
                  name="academicalGroup"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Академическая группа</FormLabel>
                      <Input {...field} placeholder="РИ-123456" />
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="email"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Почта</FormLabel>
                      <Input {...field} placeholder="johndoe@mail.ru" />
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="phone"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Телефон</FormLabel>
                      <Input {...field} placeholder="+7 (123) 456 78 90" />
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="telegram"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Телеграм</FormLabel>
                      <Input placeholder="johndoe" {...field} />
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <DialogFooter>
                  <Button type="submit">Добавить</Button>
                </DialogFooter>
              </form>
            </Form>
          </DialogContent>
        </Dialog>
        <Input placeholder="Фильтр" className="w-fit" />
      </div>
      <DataTable columns={columns} data={mockData} />
    </div>
  );
}
