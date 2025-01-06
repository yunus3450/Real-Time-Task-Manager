import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { fetchTasks } from "../store/taskSlice";

const TaskList = () => {
  const dispatch = useDispatch();
  const tasks = useSelector((state) => state.tasks.tasks);
  const status = useSelector((state) => state.tasks.status);

  useEffect(() => {
    if (status === "idle") {
      dispatch(fetchTasks());
    }
  }, [dispatch, status]);

  return (
    <div>
      <h1>Görev Listesi</h1>
      {status === "loading" && <p>Yükleniyor...</p>}
      {status === "failed" && <p>Hata oluştu!</p>}
      <ul>
        {tasks.map((task) => (
          <li key={task.id}>{task.title}</li>
        ))}
      </ul>
    </div>
  );
};

export default TaskList;
