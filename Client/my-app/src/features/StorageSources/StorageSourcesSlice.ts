import {createSlice} from "@reduxjs/toolkit";

const initialState = {
    sources: ["MsSql Db", "XML"],
    currentSource: "MsSql DB"
}

const storageSourcesSlice = createSlice({
    name: 'storageSource',
    initialState,
    reducers: {
        sourceChanged(state, action){
            const {source} = action.payload;
            if(state.sources.includes(source)){
                state.currentSource = source;
            }
        }
    }
})

export const {sourceChanged} = storageSourcesSlice.actions;

export default storageSourcesSlice.reducer;