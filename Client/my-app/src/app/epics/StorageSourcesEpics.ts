import {
    changeSource,
    fetchStorageSourcesData,
    sourceChanged,
    storageSourcesDataAdded
} from "../../features/StorageSources/StorageSourcesSlice";
import {combineEpics, Epic, ofType} from "redux-observable";
import {RootState} from "../store";
import {from, map, mergeMap} from "rxjs";
import {client} from "../../GraphQl/client";
import {CHANGE_SOURCE} from "../../GraphQl/StorageSource/mutations";
import {FETCH_SOURCES_DATA} from "../../GraphQl/StorageSource/queries";

export const changeStorageSourceEpic: Epic<ReturnType<typeof changeSource>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("storageSource/changeSource"),
        mergeMap(action => from(client.mutate(
            {
                mutation: CHANGE_SOURCE,
                variables: {source: action.payload.source}
            }
        )).pipe(map(response => sourceChanged(response.data.storage.setSource))))
    )
}

export const fetchStorageSourcesDataEpic: Epic<ReturnType<typeof fetchStorageSourcesData>, any, RootState> = action$ => {
    return action$.pipe(
        ofType("storageSource/fetchSourcesData"),
        mergeMap(action =>
            from(client.query({
                    query: FETCH_SOURCES_DATA
                })
            ).pipe(map(response => storageSourcesDataAdded(response.data.storageSources.getStorageSourcesData)))))
}

// @ts-ignore
export const StorageSourceEpics = combineEpics(changeStorageSourceEpic, fetchStorageSourcesDataEpic);