import { Button } from "@/components/ui/button";
import {
  DialogClose,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import type { ICase } from "@/features/cases/types";
import { getInitials } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

const schema = z.object({
  name: z.string().max(50),
  description: z.string().max(1000),
});

export default function Application({ case: initialCase }: { case: ICase }) {
  const form = useForm<z.infer<typeof schema>>({
    resolver: zodResolver(schema),
    defaultValues: {
      name: initialCase.name,
      description: initialCase.description,
    },
  });

  console.log({ initialCase });

  function onSubmit(values: z.infer<typeof schema>) {
    console.log(values);
  }

  return (
    <>
      <DialogHeader>
        <DialogTitle>Заявка</DialogTitle>
        <DialogDescription>
          Автор: {getInitials(initialCase.author)}
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
                  <Input {...field} />
                </FormControl>
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
              </FormItem>
            )}
          />
          <DialogFooter>
            <DialogClose asChild>
              <Button type="submit">Сохранить</Button>
            </DialogClose>
          </DialogFooter>
        </form>
      </Form>
    </>
  );
}
