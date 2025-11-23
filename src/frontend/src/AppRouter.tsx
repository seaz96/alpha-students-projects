import { Route } from "react-router";
import { BrowserRouter, Routes } from "react-router";
import AppLayout from "./AppLayout";
import Home from "./pages/Home/Home";
import NotFound from "./pages/NotFound/NotFound";
import SignIn from "./pages/SignIn/SignIn";
import Applications from "./pages/Applications/Applications";
import AuthOnly from "./components/auth-only";
import ProfilePage from "./pages/ProfilePage/ProfilePage";
import Projects from "./pages/Projects/Projects";

export default function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<AppLayout />}>
          <Route path="/" element={<AuthOnly />}>
            <Route index element={<Home />} />
            <Route path="/profile" element={<ProfilePage />} />
            <Route path="/applications" element={<Applications />} />
            <Route path="/projects" element={<Projects />} />
          </Route>
          <Route path="/sign-in" element={<SignIn />} />
          <Route path="*" element={<NotFound />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
