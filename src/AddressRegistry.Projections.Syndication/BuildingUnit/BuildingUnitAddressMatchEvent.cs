namespace AddressRegistry.Projections.Syndication.BuildingUnit
{
    public enum BuildingEvent
    {
        BuildingBecameComplete,
        BuildingBecameIncomplete,
        BuildingBecameUnderConstruction,
        BuildingGeometryWasRemoved,
        BuildingMeasurementByGrbWasCorrected,
        BuildingOutlineWasCorrected,
        BuildingPersistentLocalIdentifierWasAssigned,
        BuildingStatusWasCorrectedToRemoved,
        BuildingStatusWasRemoved,
        BuildingWasCorrectedToNotRealized,
        BuildingWasCorrectedToPlanned,
        BuildingWasCorrectedToRealized,
        BuildingWasCorrectedToRetired,
        BuildingWasCorrectedToUnderConstruction,
        BuildingWasMeasuredByGrb,
        BuildingWasNotRealized,
        BuildingWasOutlined,
        BuildingWasPlanned,
        BuildingWasRealized,
        BuildingWasRetired,
        BuildingWasRegistered,
        BuildingWasRemoved,

        BuildingUnitAddressWasAttached,
        BuildingUnitAddressWasDetached,
        BuildingUnitBecameComplete,
        BuildingUnitBecameIncomplete,
        BuildingUnitPersistentLocalIdentifierWasAssigned,
        BuildingUnitPersistentLocalIdentifierWasRemoved,
        BuildingUnitPersistentLocalIdentifierWasDuplicated,
        BuildingUnitPositionWasAppointedByAdministrator,
        BuildingUnitPositionWasCorrectedToAppointedByAdministrator,
        BuildingUnitPositionWasCorrectedToDerivedFromObject,
        BuildingUnitPositionWasDerivedFromObject,
        BuildingUnitStatusWasRemoved,
        BuildingUnitWasAdded,
        BuildingUnitWasAddedToRetiredBuilding,
        BuildingUnitWasCorrectedToNotRealized,
        BuildingUnitWasCorrectedToPlanned,
        BuildingUnitWasCorrectedToRealized,
        BuildingUnitWasCorrectedToRetired,
        BuildingUnitWasNotRealized,
        BuildingUnitWasNotRealizedByBuilding,
        BuildingUnitWasNotRealizedByParent,
        BuildingUnitWasPlanned,
        BuildingUnitWasReaddedByOtherUnitRemoval,
        BuildingUnitWasReaddressed,
        BuildingUnitWasRealized,
        BuildingUnitWasRemoved,
        BuildingUnitWasRetired,
        BuildingUnitWasRetiredByParent,
        CommonBuildingUnitWasAdded,
    }
}
