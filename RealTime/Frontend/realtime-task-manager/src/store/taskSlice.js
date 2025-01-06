import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axiosClient from "../api/axiosClient";
export const fetchTasks = createAsyncThunk("tasks/fetchTasks", async () => {
  const response = await axiosClient.get("/tasks");
  return response.data;
});

export const addTask = createAsyncThunk("tasks/addTask", async (task) => {
  const response = await axiosClient.post("/tasks", task);
  return response.data;
});

const taskSlice = createSlice({
  name: "tasks",
  initialState: {
    tasks: [],
    status: "idle",
    error: null,
  },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchTasks.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchTasks.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.tasks = action.payload;
      })
      .addCase(fetchTasks.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(addTask.fulfilled, (state, action) => {
        state.tasks.push(action.payload);
      });
  },
});

export default taskSlice.reducer;
