import React from "react";
import {changeSource, sourceChanged} from "./StorageSourcesSlice";
import {useAppSelector, useAppDispatch} from '../../app/hooks'
import {StorageSourceChangeInputType} from "../../GraphQl/StorageSource/mutations";


export const StoragePicker = () => {
    const dispatch = useAppDispatch();
    const storageSources = useAppSelector(state => state.storageSources);
    const sources = storageSources.sources;
    const currentCourse = storageSources.currentSource;


    const onSourceChanged = (e: React.FormEvent<HTMLSelectElement>) => {
        const StorageSourceChangeInput: StorageSourceChangeInputType = {
            source: e.currentTarget.value
        }

        dispatch(
            changeSource(StorageSourceChangeInput)
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