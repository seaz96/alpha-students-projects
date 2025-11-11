import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import CreateApplication from "./CreateApplication";
import ApplicationsDataTable from "./ApplicationsDataTable";

export default function Applications() {
  return (
    <div className="mx-auto max-w-screen-xl px-8 py-4">
      <h1 className="scroll-m-20 text-4xl font-semibold tracking-tight text-balance">
        Заявки на проекты
      </h1>
      <Tabs defaultValue="applications" className="mt-4">
        <TabsList>
          <TabsTrigger value="applications">Заявки</TabsTrigger>
          <TabsTrigger value="cases">Кейсы</TabsTrigger>
        </TabsList>
        <TabsContent value="applications">
          <CreateApplication />
          <ApplicationsDataTable />
        </TabsContent>
        <TabsContent value="cases"></TabsContent>
      </Tabs>
    </div>
  );
}
