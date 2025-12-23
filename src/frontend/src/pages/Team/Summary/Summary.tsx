import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { Spinner } from "@/components/ui/spinner";
import {
  useGetTeamResultQuery,
  usePatchTeamResultMutation,
} from "@/features/teams/teamsApi";
import { SaveIcon } from "lucide-react";
import { useEffect, useState } from "react";
import { toast } from "sonner";

export default function Summary({ teamId }: { teamId: string }) {
  const {
    data: result,
    isLoading,
    error,
    isFetching,
  } = useGetTeamResultQuery({ teamId });

  const [patchTeamResult, { isLoading: isSaving }] =
    usePatchTeamResultMutation();

  const [score, setScore] = useState<string>("");
  const [comment, setComment] = useState<string>("");

  const isNotFound = error && (error as { status: number }).status === 404;
  const isRealError = error && !isNotFound;

  useEffect(() => {
    if (result) {
      setScore(result.score?.toString() ?? "");
      setComment(result.comment ?? "");
    } else if (isNotFound) {
      setScore("");
      setComment("");
    }
  }, [result, isNotFound]);

  const handleSave = async () => {
    try {
      const numericScore = score === "" ? null : Number(score);

      await patchTeamResult({
        teamId,
        comment,
        score: numericScore,
      }).unwrap();

      toast.success("Результаты команды обновлены");
    } catch {
      toast.error("Не удалось сохранить результаты");
    }
  };

  if (isLoading) {
    return (
      <div className="flex justify-center p-4">
        <Spinner />
      </div>
    );
  }

  if (isRealError) {
    return (
      <div className="text-destructive border-destructive rounded-md border p-4">
        Произошла ошибка при загрузке результатов.
      </div>
    );
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>Результаты команды</CardTitle>
        <CardDescription>Оценка и комментарий ментора</CardDescription>
      </CardHeader>
      <CardContent className="space-y-4">
        <div className="space-y-2">
          <Label htmlFor="score">Оценка</Label>
          <Input
            id="score"
            type="number"
            placeholder="Введите баллы (0-100)"
            value={score}
            onChange={(e) => setScore(e.target.value)}
          />
        </div>
        <div className="space-y-2">
          <Label htmlFor="comment">Комментарий</Label>
          <Textarea
            id="comment"
            placeholder="Опишите сильные и слабые стороны..."
            value={comment}
            onChange={(e) => setComment(e.target.value)}
            rows={5}
          />
        </div>
      </CardContent>
      <CardFooter className="flex justify-between">
        <div className="text-muted-foreground text-xs">
          {isNotFound && "Результат еще не создан"}
        </div>
        <Button onClick={handleSave} disabled={isSaving || isFetching}>
          {isSaving ? (
            <Spinner className="mr-2 h-4 w-4" />
          ) : (
            <SaveIcon className="mr-2 h-4 w-4" />
          )}
          Сохранить
        </Button>
      </CardFooter>
    </Card>
  );
}
