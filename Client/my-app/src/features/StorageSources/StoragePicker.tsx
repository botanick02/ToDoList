import React from "react";
import {sourceChanged} from "./StorageSourcesSlice";
import { useAppSelector, useAppDispatch } from '../../app/hooks'


export const StoragePicker = () => {
    const dispatch = useAppDispatch();
    const storageSources = useAppSelector(state => state.storageSources);
    const sources = storageSources.sources;
    const currentCourse = storageSources.currentSource;


    const onSourceChanged = (e: React.FormEvent<HTMLSelectElement>) => {
        dispatch(
            sourceChanged({
                source: e.currentTarget.value
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