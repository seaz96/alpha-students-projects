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
import Project from "./pages/Project/Project";
import Team from "./pages/Team/Team";
import Dashboard from "./pages/Dashboard/Dashboard";
import StudentPositions from "./pages/Dashboard/StudentPositions";

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
            <Route path="/projects/:id" element={<Project />} />
            <Route
              path="/projects/:projectId/teams/:teamId"
              element={<Team />}
            />

            <Route path="/dashboard" element={<Dashboard />}>
              <Route
                path="/dashboard/student-roles"
                element={<StudentPositions />}
              />
            </Route>
          </Route>
          <Route path="/sign-in" element={<SignIn />} />
          <Route path="*" element={<NotFound />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
