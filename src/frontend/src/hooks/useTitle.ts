import { useEffect } from "react";

const useTitle = (title: string) => {
  useEffect(() => {
    const previousTitle = document.title;
    document.title = title + " | " + import.meta.env.VITE_APP_NAME;
    return () => {
      document.title = previousTitle;
    };
  }, [title]);
};

export default useTitle;
