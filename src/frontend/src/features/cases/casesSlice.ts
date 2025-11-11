import { createAppSlice } from "@/app/createAppSlice";
import { casesApi } from "./casesApi";
import type { CasesState } from "./types";

const initialState: CasesState = {
  cases: [],
};

export const casesSlice = createAppSlice({
  name: "cases",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addMatcher(
        casesApi.endpoints.getCases.matchFulfilled,
        (state, action) => {
          state.cases = action.payload;
        },
      )
      .addMatcher(
        casesApi.endpoints.createCase.matchFulfilled,
        (state, action) => {
          state.cases.push(action.payload);
        },
      );
    //
    // .addMatcher(
    //   casesApi.endpoints.deleteCase.matchFulfilled,
    //   (state, action) => {
    //     state.cases = state.cases.filter(
    //                                               ↓ TODO: return deleted case in response
    //       (caseItem) => caseItem.id !== action.payload,
    //     );
    //   },
    // );
  },
  selectors: {
    selectCases: (state) => state.cases,
  },
});

export const { selectCases } = casesSlice.selectors;
