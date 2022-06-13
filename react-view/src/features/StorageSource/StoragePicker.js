import React from "react";
import {useDispatch, useSelector} from "react-redux";
import {sourceChanged} from "./StorageSourcesSlice";

export default function StoragePicker() {
    const dispatch = useDispatch();
    const storageSources = useSelector(state => state.storageSources);
    const sources = storageSources.sources;
    const currentCourse = storageSources.currentSource;


    const onSourceChanged = e => {
        dispatch(
            sourceChanged({
                source: e.target.value
            })
        )
    }

    return (
        <form className={"storage-picker"}>
            <select value={currentCourse} onChange={onSourceChanged}>
                {sources.map(s => (
                    <option key={s} value={s}>{s}</option>
                ))}
            </select>
        </form>
    )
}