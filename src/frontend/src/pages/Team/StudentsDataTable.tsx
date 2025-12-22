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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { useGetPositionsQuery } from "@/features/studentPositions/studentPositionApi";
import type { IStudentPosition } from "@/features/studentPositions/types";
import {
  useAddStudentMutation,
  useDeleteStudentMutation,
  usePatchStudentMutation,
} from "@/features/teams/teamsApi";
import type { IStudent } from "@/features/teams/types";
import { formatPhoneNumber } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import type { ColumnDef } from "@tanstack/react-table";
import { Edit2Icon, TrashIcon, User2Icon } from "lucide-react";
import { useCallback, useMemo, useState } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router";
import z from "zod";

export function StudentsDataTable({ students }: { students: IStudent[] }) {
  const { teamId } = useParams();
  const [addStudent] = useAddStudentMutation();

  const { data: positionsData } = useGetPositionsQuery({
    limit: 999,
    offset: 0,
  });
  const positions = useMemo(
    () => positionsData?.data || [],
    [positionsData?.data],
  );

  const columns = useMemo<ColumnDef<IStudent>[]>(
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
        accessorKey: "position",
        header: "Роль",
        cell: ({ row }) => <p>{row.original.position.name}</p>,
      },
      {
        accessorKey: "academicGroup",
        header: "Группа",
        cell: ({ row }) => <p>{row.original.academicGroup}</p>,
      },
      {
        accessorKey: "email",
        header: "Почта",
        cell: ({ row }) => <p>{row.original.email}</p>,
      },
      {
        accessorKey: "phone",
        header: "Телефон",
        cell: ({ row }) => <p>{formatPhoneNumber(row.original.phone)}</p>,
      },
      {
        accessorKey: "telegram",
        header: "Telegram",
        cell: ({ row }) => <p>{row.original.telegram}</p>,
      },
      {
        accessorKey: "controls",
        header: "",
        cell: ({ row }) => (
          <StudentRowActions student={row.original} positions={positions} />
        ),
      },
    ],
    [positions],
  );

  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const schema = z.object({
    firstName: z.string().min(1, "Обязательное поле").trim(),
    middleName: z.string().min(1, "Обязательное поле").trim(),
    lastName: z.string().min(1, "Обязательное поле").trim(),
    positionId: z.string("Выберите роль").min(1, "Выберите роль"),
    academicGroup: z
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
      academicGroup: "",
      email: "",
      firstName: "",
      lastName: "",
      middleName: "",
      phone: "",
      telegram: "",
      positionId: "",
    },
  });

  const onSubmit = useCallback(
    async (values: z.infer<typeof schema>) => {
      await addStudent({ ...values, teamId: teamId! });
      form.reset();
      setIsDialogOpen(false);
    },
    [addStudent, form, teamId],
  );

  return (
    <div className="flex flex-col gap-1">
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

                <div className="flex gap-2">
                  <FormField
                    control={form.control}
                    name="academicGroup"
                    render={({ field }) => (
                      <FormItem className="flex-1">
                        <FormLabel>Академическая группа</FormLabel>
                        <Input {...field} placeholder="РИ-123456" />
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name="positionId"
                    render={({ field }) => (
                      <FormItem className="flex-1">
                        <FormLabel>Роль</FormLabel>
                        <Select
                          onValueChange={field.onChange}
                          defaultValue={field.value}
                        >
                          <FormControl>
                            <SelectTrigger className="w-full">
                              <SelectValue placeholder="Выберите роль" />
                            </SelectTrigger>
                          </FormControl>
                          <SelectContent>
                            {positions.map((pos) => (
                              <SelectItem key={pos.id} value={pos.id}>
                                {pos.name}
                              </SelectItem>
                            ))}
                          </SelectContent>
                        </Select>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                </div>

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
      <DataTable columns={columns} data={students} />
    </div>
  );
}

const patchSchema = z.object({
  firstName: z.string().min(1, "Обязательное поле").trim(),
  middleName: z.string().min(1, "Обязательное поле").trim(),
  lastName: z.string().min(1, "Обязательное поле").trim(),
  positionId: z.string("Выберите роль").min(1, "Выберите роль"),
  academicGroup: z
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

function StudentRowActions({
  student,
  positions,
}: {
  student: IStudent;
  positions: IStudentPosition[];
}) {
  const { teamId } = useParams();
  const [deleteStudent] = useDeleteStudentMutation();
  const [patchStudent] = usePatchStudentMutation();
  const [isOpen, setIsOpen] = useState(false);

  const form = useForm<z.infer<typeof patchSchema>>({
    resolver: zodResolver(patchSchema),
    defaultValues: {
      firstName: student.firstName,
      middleName: student.middleName,
      lastName: student.lastName,
      academicGroup: student.academicGroup,
      email: student.email,
      phone: student.phone,
      telegram: student.telegram,
      positionId: student.position.id,
    },
  });

  const handleOpenChange = (open: boolean) => {
    if (open) {
      form.reset({
        firstName: student.firstName,
        middleName: student.middleName,
        lastName: student.lastName,
        academicGroup: student.academicGroup,
        email: student.email,
        phone: student.phone,
        telegram: student.telegram,
        positionId: student.position.id,
      });
    }
    setIsOpen(open);
  };

  const onSubmit = async (values: z.infer<typeof patchSchema>) => {
    await patchStudent({
      id: student.id,
      teamId: teamId!,
      ...values,
    });
    setIsOpen(false);
  };

  return (
    <div className="flex gap-1">
      <Dialog open={isOpen} onOpenChange={handleOpenChange}>
        <DialogTrigger asChild>
          <Button size="icon-sm" variant="outline">
            <Edit2Icon className="size-4" />
          </Button>
        </DialogTrigger>
        <DialogContent aria-describedby="">
          <DialogHeader>
            <DialogTitle>Редактировать студента</DialogTitle>
          </DialogHeader>
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-2">
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

              {/* Группа и Роль в одну строку */}
              <div className="flex gap-2">
                <FormField
                  control={form.control}
                  name="academicGroup"
                  render={({ field }) => (
                    <FormItem className="flex-1">
                      <FormLabel>Академическая группа</FormLabel>
                      <Input {...field} placeholder="РИ-123456" />
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="positionId"
                  render={({ field }) => (
                    <FormItem className="flex-1">
                      <FormLabel>Роль</FormLabel>
                      <Select
                        onValueChange={field.onChange}
                        value={field.value}
                      >
                        <FormControl>
                          <SelectTrigger className="w-full">
                            <SelectValue placeholder="Выберите роль" />
                          </SelectTrigger>
                        </FormControl>
                        <SelectContent>
                          {positions.map((pos) => (
                            <SelectItem key={pos.id} value={pos.id}>
                              {pos.name}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>

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
                <Button type="submit">Сохранить</Button>
              </DialogFooter>
            </form>
          </Form>
        </DialogContent>
      </Dialog>

      <Button
        size="icon-sm"
        variant="outline"
        onClick={() => deleteStudent({ teamId: teamId!, id: student.id })}
      >
        <TrashIcon className="size-4" />
      </Button>
    </div>
  );
}
