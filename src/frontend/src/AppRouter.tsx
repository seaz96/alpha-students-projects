import { Route } from "react-router";
import { BrowserRouter, Routes } from "react-router";
import AppLayout from "./AppLayout";
import Home from "./pages/Home/Home";

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<AppLayout />}>
          <Route index element={<Home />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
