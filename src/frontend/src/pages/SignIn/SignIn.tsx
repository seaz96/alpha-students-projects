import { Input } from "@/components/ui/input";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Button } from "@/components/ui/button";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";

const crediantialsSchema = z.object({
  username: z
    .string()
    .min(2, { error: "Имя пользователя должно быть длиннее 2 символов" }),
  password: z
    .string()
    .min(6, { error: "Пароль должен быть длиннее 6 символов" }),
});

export default function SignIn() {
  const registerForm = useForm<z.infer<typeof crediantialsSchema>>({
    resolver: zodResolver(crediantialsSchema),
    defaultValues: {
      username: "",
      password: "",
    },
  });

  const loginForm = useForm<z.infer<typeof crediantialsSchema>>({
    resolver: zodResolver(crediantialsSchema),
    defaultValues: {
      username: "",
      password: "",
    },
  });

  function onSubmitLogin(values: z.infer<typeof crediantialsSchema>) {
    console.log(values);
  }

  function onSubmitRegister(values: z.infer<typeof crediantialsSchema>) {
    console.log(values);
  }

  return (
    <div className="mt-12 flex items-center justify-center">
      <div className="w-full max-w-md rounded-lg border p-8 shadow-lg">
        <h1 className="mb-4 text-center text-2xl font-bold">
          Войдите в аккаунт
        </h1>

        <Tabs defaultValue="signin">
          <TabsList className="w-full">
            <TabsTrigger value="signin">Вход</TabsTrigger>
            <TabsTrigger value="signup">Регистрация</TabsTrigger>
          </TabsList>
          <TabsContent value="signin">
            <Form {...loginForm}>
              <form
                onSubmit={loginForm.handleSubmit(onSubmitLogin)}
                className="space-y-6"
              >
                <FormField
                  control={loginForm.control}
                  name="username"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Логин</FormLabel>
                      <FormControl>
                        <Input placeholder="Ваш логин" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                ></FormField>
                <FormField
                  control={loginForm.control}
                  name="password"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Пароль</FormLabel>
                      <FormControl>
                        <Input placeholder="Ваш пароль" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <Button type="submit" className="w-full">
                  Войти
                </Button>
              </form>
            </Form>
          </TabsContent>
          <TabsContent value="signup">
            <Form {...registerForm}>
              <form
                onSubmit={registerForm.handleSubmit(onSubmitRegister)}
                className="space-y-6"
              >
                <FormField
                  control={registerForm.control}
                  name="username"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Логин</FormLabel>
                      <FormControl>
                        <Input placeholder="Ваш логин" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                ></FormField>
                <FormField
                  control={registerForm.control}
                  name="password"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Пароль</FormLabel>
                      <FormControl>
                        <Input placeholder="Ваш пароль" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <Button type="submit" className="w-full">
                  Зарегистрироваться
                </Button>
              </form>
            </Form>
          </TabsContent>
        </Tabs>
      </div>
    </div>
  );
}
