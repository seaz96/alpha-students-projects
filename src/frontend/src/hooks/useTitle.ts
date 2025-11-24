import { useEffect } from "react";

const useTitle = (title: string | undefined) => {
  useEffect(() => {
    const previousTitle = document.title;
    if (title !== undefined)
      document.title = title + " | " + import.meta.env.VITE_APP_NAME;
    else document.title = import.meta.env.VITE_APP_NAME;
    return () => {
      document.title = previousTitle;
    };
  }, [title]);
};

export default useTitle;
