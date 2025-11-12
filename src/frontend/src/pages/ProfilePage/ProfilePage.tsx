import { useAppSelector } from "@/app/hooks";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { usePatchUserInfoMutation } from "@/features/users/usersApi";
import { selectUser } from "@/features/users/usersSlice";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import z from "zod";

const re = /^[а-яА-ЯёЁa-zA-Z'-]+$/;

const userSchema = z.object({
  lastName: z.string().trim().regex(re, {
    error: "Может содержать только буквы, дефис и апостроф",
  }),
  firstName: z.string().trim().regex(re, {
    error: "Может содержать только буквы, дефис и апостроф",
  }),
  middleName: z.string().trim().regex(re, {
    error: "Может содержать только буквы, дефис и апостроф",
  }),
});

export default function ProfilePage() {
  const user = useAppSelector(selectUser);

  const [patchUserInfo] = usePatchUserInfoMutation();

  const userForm = useForm<z.infer<typeof userSchema>>({
    resolver: zodResolver(userSchema),
    defaultValues: {
      lastName: user?.lastName,
      firstName: user?.firstName,
      middleName: user?.middleName,
    },
  });

  function onSubmitUserForm(values: z.infer<typeof userSchema>) {
    patchUserInfo(values);
  }

  return (
    <div className="mx-auto max-w-screen-xl px-8 py-4">
      <h1 className="scroll-m-20 text-4xl font-semibold tracking-tight text-balance">
        Мой профиль
      </h1>
      <Form {...userForm}>
        <form
          onSubmit={userForm.handleSubmit(onSubmitUserForm)}
          className="mt-6 max-w-96 space-y-4"
        >
          <div className="grid gap-1">
            <Label>Почта</Label>
            <Input disabled value={user?.email} />
          </div>
          <FormField
            control={userForm.control}
            name="middleName"
            render={({ field }) => (
              <FormItem className="gap-1">
                <FormLabel>Фамилия</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={userForm.control}
            name="firstName"
            render={({ field }) => (
              <FormItem className="gap-1">
                <FormLabel>Имя</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={userForm.control}
            name="lastName"
            render={({ field }) => (
              <FormItem className="gap-1">
                <FormLabel>Отчество</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button>Сохранить</Button>
        </form>
      </Form>
    </div>
  );
}
