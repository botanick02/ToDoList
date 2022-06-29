import {createAction, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {CategoryUpdateInputType} from "../../GraphQl/Categories/mutations";
import {InputType} from "zlib";
import {StorageSourceChangeInputType} from "../../GraphQl/StorageSource/mutations";
import {StorageSource} from "./types/StorageSource";

type storageSourceState = {
    sources: string[],
    currentSource: string
}

const initialState = {
    sources: ["Database", "XML"],
    currentSource: "Database"
}

const storageSourcesSlice = createSlice({
    name: 'storageSource',
    initialState,
    reducers: {
        sourceChanged(state, action: PayloadAction<StorageSource>) {
            const {currentSource} = action.payload;
            if (state.sources.includes(currentSource)) {
                state.currentSource = currentSource;
            }
        },
        storageSourcesDataAdded(state, action: PayloadAction<StorageSource>) {
            const {storageSources, currentSource} = action.payload;
            state.sources = storageSources;
            state.currentSource = currentSource;
        }
    }
})

export const {sourceChanged, storageSourcesDataAdded} = storageSourcesSlice.actions;

export const changeSource = createAction<StorageSourceChangeInputType>("storageSource/changeSource");
export const fetchStorageSourcesData = createAction("storageSource/fetchSourcesData");



export default storageSourcesSlice.reducer;