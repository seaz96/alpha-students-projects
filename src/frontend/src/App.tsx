import { Provider } from "react-redux";
import { store } from "./app/store";
import AppRouter from "./AppRouter";
import { ThemeProvider } from "./components/theme-provider";

function App() {
  return (
    <>
      <Provider store={store}>
        <ThemeProvider defaultTheme="system" storageKey="ui-theme">
          <AppRouter />
        </ThemeProvider>
      </Provider>
    </>
  );
}

export default App;
