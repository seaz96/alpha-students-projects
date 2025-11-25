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
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Textarea } from "@/components/ui/textarea";
import { useCreateTeamMutation } from "@/features/teams/teamsApi";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

const formSchema = z.object({
  name: z.string().min(1, "Название не может быть пустым"),
  description: z.string(),
  projectId: z.string(),
  teamprojectLink: z.string().url("Неверный формат ссылки"),
});

export default function CreateTeamPopover({
  projectId,
}: {
  projectId: string;
}) {
  const [createTeam] = useCreateTeamMutation();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
      description: "",
      teamprojectLink: "",
      projectId,
    },
  });

  function onSubmit(values: z.infer<typeof formSchema>) {
    createTeam(values);
    form.reset();
  }

  return (
    <Popover>
      <PopoverTrigger asChild>
        <Button size="sm" className="mt-4 mb-2">
          Создать команду
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-80">
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <FormField
              control={form.control}
              name="name"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Название</FormLabel>
                  <FormControl>
                    <Input {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="description"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Описание</FormLabel>
                  <FormControl>
                    <Textarea {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="teamprojectLink"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Ссылка на проект</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="https://teamproject.urfu.ru/..."
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <Button type="submit">Создать</Button>
          </form>
        </Form>
      </PopoverContent>
    </Popover>
  );
}
