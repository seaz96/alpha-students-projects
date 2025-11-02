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
import { useNavigate } from "react-router";
import { useEffect } from "react";
import {
  useLoginMutation,
  useRegisterMutation,
} from "@/features/users/usersApi";

const crediantialsSchema = z.object({
  email: z.email(),
  password: z
    .string()
    .min(8, { error: "Пароль должен быть длиннее 8 символов" }),
});

export default function SignIn() {
  const [login, { isSuccess: isSuccessLogin }] = useLoginMutation();
  const [register, { isSuccess: isSuccessRegister }] = useRegisterMutation();
  const navigate = useNavigate();

  useEffect(() => {
    if (isSuccessLogin || isSuccessRegister) {
      navigate("/");
    }
  }, [isSuccessLogin, isSuccessRegister, navigate]);

  const registerForm = useForm<z.infer<typeof crediantialsSchema>>({
    resolver: zodResolver(crediantialsSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const loginForm = useForm<z.infer<typeof crediantialsSchema>>({
    resolver: zodResolver(crediantialsSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  function onSubmitLogin(values: z.infer<typeof crediantialsSchema>) {
    login(values);
  }

  function onSubmitRegister(values: z.infer<typeof crediantialsSchema>) {
    register(values);
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
                  name="email"
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
                        <Input
                          type="password"
                          placeholder="Ваш пароль"
                          {...field}
                        />
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
                  name="email"
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
                        <Input
                          type="password"
                          placeholder="Ваш пароль"
                          {...field}
                        />
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
