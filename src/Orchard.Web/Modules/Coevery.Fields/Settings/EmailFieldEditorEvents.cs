﻿using System.Collections.Generic;
using Coevery.Entities.Settings;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;

namespace Coevery.Fields.Settings {
    public class EmailFieldEditorEvents : FieldEditorEvents {
        public override IEnumerable<TemplateViewModel> FieldDescriptor() {
            var model = string.Empty;
            yield return DisplayTemplate(model, "Email", null);
        }

        public override IEnumerable<TemplateViewModel> PartFieldEditor(ContentPartFieldDefinition definition) {
            if (definition.FieldDefinition.Name == "EmailField"
                || definition.FieldDefinition.Name == "EmailFieldCreate") {
                var model = definition.Settings.GetModel<EmailFieldSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> PartFieldEditorUpdate(ContentPartFieldDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.FieldType != "EmailField") {
                yield break;
            }

            var model = new EmailFieldSettings();
            if (updateModel.TryUpdateModel(model, "EmailFieldSettings", null, null)) {
                UpdateSettings(model, builder, "EmailFieldSettings");
                builder.WithSetting("EmailFieldSettings.DefaultValue", model.DefaultValue);
            }

            yield return DefinitionTemplate(model);
        }
    }
}