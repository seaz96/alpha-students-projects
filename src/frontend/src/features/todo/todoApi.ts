import { baseQuery } from "@/api/baseQuery";
import { createApi } from "@reduxjs/toolkit/query/react";
import { type GetResponse } from "@/types";
import type { ITodo, PostTodoArgs, PatchTodoArgs, GetTodosArgs } from "./types";

const getIdsFromTree = (nodes: ITodo[]): string[] => {
  return nodes.flatMap((node) => [
    node.id,
    ...(node.children ? getIdsFromTree(node.children) : []),
  ]);
};

export const todosApi = createApi({
  baseQuery,
  reducerPath: "todosApi",
  tagTypes: ["Todo"],
  endpoints: (builder) => ({
    getTodos: builder.query<GetResponse<ITodo>, GetTodosArgs>({
      query: (params) => ({
        url: "/todos",
        method: "GET",
        credentials: "include",
        params,
      }),
      transformResponse: (response: GetResponse<ITodo>) => {
        const todos = response.data.map((item) => ({
          ...item,
          children: [] as ITodo[],
        }));

        const map: Record<string, ITodo> = {};
        const roots: ITodo[] = [];

        todos.forEach((todo) => {
          map[todo.id] = todo;
        });

        todos.forEach((todo) => {
          if (todo.parentId && map[todo.parentId]) {
            map[todo.parentId].children!.push(todo);
          } else {
            roots.push(todo);
          }
        });

        return {
          ...response,
          data: roots,
        };
      },
      providesTags: (res) =>
        res
          ? [
              ...getIdsFromTree(res.data).map((id) => ({
                type: "Todo" as const,
                id,
              })),
              { type: "Todo", id: "LIST" },
            ]
          : [{ type: "Todo", id: "LIST" }],
    }),
    createTodo: builder.mutation<ITodo, PostTodoArgs>({
      query: (body) => ({
        url: "/todos",
        method: "POST",
        credentials: "include",
        body,
      }),
      invalidatesTags: [{ type: "Todo", id: "LIST" }],
    }),
    patchTodo: builder.mutation<ITodo, PatchTodoArgs>({
      query: ({ todoId, ...body }) => ({
        url: "/todos/" + todoId,
        method: "PATCH",
        credentials: "include",
        body,
      }),
      invalidatesTags: (_res, _err, { todoId }) => [
        { type: "Todo", id: todoId },
      ],
    }),
    getTodo: builder.query<ITodo, { id: string }>({
      query: ({ id }) => ({
        url: "/todos/" + id,
        method: "GET",
        credentials: "include",
      }),
      providesTags: (_res, _err, { id }) => [{ type: "Todo", id }],
    }),
    deleteTodo: builder.mutation<ITodo, { id: string }>({
      query: ({ id }) => ({
        url: "/todos/" + id,
        method: "DELETE",
        credentials: "include",
      }),
      invalidatesTags: (_res, _err, { id }) => [{ type: "Todo", id }],
    }),
  }),
});

export const {
  useGetTodosQuery,
  useCreateTodoMutation,
  usePatchTodoMutation,
  useGetTodoQuery,
  useDeleteTodoMutation,
} = todosApi;
