import { Route } from "react-router";
import { BrowserRouter, Routes } from "react-router";
import AppLayout from "./AppLayout";
import Home from "./pages/Home/Home";
import NotFound from "./pages/NotFound/NotFound";

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<AppLayout />}>
          <Route index element={<Home />} />
          <Route path="*" element={<NotFound />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
